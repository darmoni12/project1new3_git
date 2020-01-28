using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Net.Mail;
using System.ComponentModel;
using System.Threading;

namespace BL
{
    public class BL_imp : IBL
    {
        Idal dal = Dali.GetDal();
        public BL_imp()
        {
            Thread updateOrderThread = new Thread(updateOrder);//פעם ביום מעדכן הזמנות רלוונטיות
            updateOrderThread.Start();
        }
        private void updateOrder()
        {
            while(true)
            {
                foreach (Order order in dal.getAllOrder())
                {
                    try
                    {
                        if (getNumOfDays(order.CreateDate) > 31)
                            rejectOrder(order);
                    }
                    catch (Exception) { }
                }
                Thread.Sleep(86400000);//a day
            }
        }
        public IEnumerable<HostingUnit> getEmptyUnits(MyDate date, int numOfDays)
        {
            MyDate last = Cloning.Clone(date);
            last.addDays(numOfDays);
            return dal.getAllUnits().Where(unit => unit.checkEmpty(date,last));
        }

        public int getNumOfDays(MyDate first, MyDate last)
        {
            MyDate temp = Cloning.Clone(first);
            int i;
            if (last.CompareTo(first) <= 0)//if last<=first
                throw new NoDaysException();
            for (i = 0; temp.CompareTo(last)!=0; i++,temp.addDays(1)) ;
            return i;
        }

        public int getNumOfDays(MyDate date)
        {
            MyDate last = MyDate.today();
            return getNumOfDays(date, last);
        }

        public int getNumOfOrder(GuestRequest req)
        {
            return dal.getAllOrder().Count(order => order.GuestRequestKey == req.GuestRequestKey);
        }

        public int getNumOfOrder(HostingUnit unit)
        {
            return dal.getAllOrder().Count(order => order.HostingUnitKey == unit.HostingUnitKey);
        }

        public IEnumerable<Order> getOrders(int days)
        {
            return dal.getAllOrder().Where(order => getNumOfDays(order.CreateDate, MyDate.today()) >= days);
        }

        public IEnumerable<GuestRequest> getRequestIf(Func<GuestRequest, bool> predicate)
        {
            return dal.getAllGuestRequest().Where(req => predicate(req));
        }

        public IEnumerable<IEnumerable<GuestRequest>> groupByNumOfPeople()
        {
            return dal.getAllGuestRequest().GroupBy(req => req.Adults + req.Children);
        }

        public IEnumerable<IEnumerable<Host>> groupByNumOfUnits()
        {
            return dal.getAllHosts().GroupBy(host => getNumOfUnits(host.HostKey));
        }

        public int getNumOfUnits(int hostKey)
        {
            return dal.getAllUnits().Count(unit => unit.OwnerHostKey == hostKey);
        }

        public IEnumerable<IEnumerable<GuestRequest>> groupReqByArea()
        {
            return dal.getAllGuestRequest().GroupBy(req => req.Area);
        }

        public IEnumerable<IEnumerable<HostingUnit>> groupUnitsByArea()
        {
            return dal.getAllUnits().GroupBy(unit => unit.Area);
        }
        public void makeOrder(int requestKey,int unitKey)
        {
            Order order = new Order()
            {
                CreateDate = MyDate.today(),
                GuestRequestKey = requestKey,
                HostingUnitKey = unitKey,
                Status = OrderStatus.MailSent
            };
            dal.addOrder(order);

            BackgroundWorker mailworker = new BackgroundWorker();
            mailworker.DoWork += new DoWorkEventHandler(sendMail);
            mailworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            object[] parameters = new object[] { getRequest(requestKey).MailAddress, getHostingUnit(unitKey) };
            // This runs the event on new background worker thread.
            mailworker.RunWorkerAsync(parameters);
        }
        public void sendMail(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    object[] parameters = e.Argument as object[];
                    string mailAddress = (string)parameters[0];
                    HostingUnit unit = (HostingUnit)parameters[1];
                    Host owner = getHost(unit.OwnerHostKey);
                    MailMessage mail = new MailMessage();//            כתובת הנמען)ניתן להוסיף יותר מאחד(
                    mail.To.Add(mailAddress);            //           הכתובת ממנה נשלח המייל //
                    mail.From = new MailAddress("dotNet5780.5029.7337@gmail.com");//           נושא ההודעה //
                    mail.Subject = "הזמנת יחידה";//         תוכן ההודעה )נניח שתוכן ההודעה בפורמט HTML //(
                    mail.Body = "יש לך הזמנה רלוונטית" + unit.ToString() + " ליצירת קשר " + owner.MailAddress + " " + owner.PhoneNumber + " " + owner.PrivateName;
                    //           הגדרה שתוכן ההודעה בפורמט HTML //
                    mail.IsBodyHtml = true;

                    //           יצירת עצם מסוג Smtp //
                    SmtpClient smtp = new SmtpClient();

                    //           הגדרת השרת של gmail //
                    smtp.Host = "smtp.gmail.com";
                    //          הגדרת פרטי הכניסה )שם משתמש וסיסמה(לחשבון ה gmail //

                    smtp.Credentials = new System.Net.NetworkCredential("dotNet5780.5029.7337@gmail.com", "idanyacov5780");
                    //          ע"פ דרישת השר, חובה לאפשר במקרה זה SSL //
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    break;
                }
                catch(Exception)
                {
                    Thread.Sleep(2000);
                }
            }
            e.Result = true;//for the thred
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;                        
        }
        public bool isInOrderList(GuestRequest request,IEnumerable<Order> orderList)
        {
            return orderList.Any(order => order.GuestRequestKey == request.GuestRequestKey);
        }
        public int acceptOrder(Order order)
        {
            if (order.Status != OrderStatus.MailSent)
                throw new orderStatusException("ההזמנה לא רלונטית להסכמה");
            GuestRequest req = getRequest(order.GuestRequestKey);
            HostingUnit unit = getHostingUnit(order.HostingUnitKey);
            updateOrderStatus(order);
            unit.updateDiary(req.EntryDate, req.ReleaseDate);
            req.ActiveStatus = false;
            dal.updateRequest(req);
            dal.updateHostingUnit(unit);
            rejectNonRelevantOrders(unit,req);
            return calculateCommission(req);
        }
        private void rejectNonRelevantOrders(HostingUnit unit, GuestRequest req)
        {
            foreach (Order order in GetOrderByUnit(unit))
            {
                if(order.Status==OrderStatus.MailSent&&!unit.fitCheck(req))//ההזמנה נשלחה אבל כבר לא רלוונטית מבחינת ימים
                {
                    rejectOrderSp(order);
                }
            } 
        }


        public GuestRequest getRequest(int reqKey)
        {
            return dal.getAllGuestRequest().FirstOrDefault(req => reqKey == req.GuestRequestKey);
        }
        public HostingUnit getHostingUnit(int unitKey)
        {
            return dal.getAllUnits().FirstOrDefault(unit => unitKey == unit.HostingUnitKey);
        }

        public int calculateCommission(GuestRequest req)
        {
            return getNumOfDays(req.EntryDate, req.ReleaseDate) * Configuration.commission;
        }
        public void rejectOrder(Order order)
        {
            if (order.Status == OrderStatus.MailSent)
            {
                order.Status = OrderStatus.NoResponsClose;
                dal.updateOrder(order);
            }
        }
        public void rejectOrderSp(Order order)
        {
            order.Status = OrderStatus.NoResponsClose;
            dal.updateOrder(order);
        }
        public void updateOrderStatus(Order order)
        {
            order.Status = OrderStatus.ReservationAprroved;
            dal.updateOrder(order);
            foreach (var tempOrder in GetOrderByReqKey(order.GuestRequestKey))
            {
                rejectOrder(tempOrder);
            }
        }
        public IEnumerable<Order> GetOrderByReqKey(int RequestKey)
        {
            return dal.getAllOrder().Where(order => order.GuestRequestKey == RequestKey);
        }
        public IEnumerable<GuestRequest> GetRequestsByUnit(HostingUnit unit)
        {
            return dal.getAllGuestRequest().Where(req => unit.fitCheck(req));
        }
        public void addRequest(GuestRequest req)
        {
            if (getNumOfDays(req.EntryDate, req.ReleaseDate) > 0) ;//if not legal throw exeption
                dal.addRequest(req);
        }
        public Host getHost(int hostKey)
        {
            return dal.getAllHosts().FirstOrDefault(host => host.HostKey == hostKey);
        }
        public void addHostingUnit(HostingUnit unit)
        {
            dal.addHostingUnit(unit);
        }
        public void addHost(Host host)
        {
            dal.addHost(host);
        }
        public void updateHostingUnit(HostingUnit unit)
        {
            if (dal.getAllOrder().Count(order => order.HostingUnitKey == unit.HostingUnitKey && order.Status == OrderStatus.MailSent) == 0)
                dal.updateHostingUnit(unit);
            else
                throw new openOrdersException("update");
        }
        public void updateHost(Host host)
        {
            dal.updateHost(host);
        }
        public void removeHostingUnit(int id)
        {
            if (dal.getAllOrder().Count(order => order.HostingUnitKey == id && order.Status != OrderStatus.NoResponsClose) == 0)//בודק שאין הזמנות פעילות ליחידת אירוח
                dal.removeHostingUnit(id);
            else
                throw new openOrdersException("לא ניתן למחוק יחידת אירוח מאחר ויש הזמנות רלונטיות");
        }
        public IEnumerable<Order> GetOrderByUnit(HostingUnit unit)
        {
            return dal.getAllOrder().Where(order => order.HostingUnitKey == unit.HostingUnitKey);
        }
        public IEnumerable<HostingUnit> getAllUnits()
        {
            return dal.getAllUnits();
        }
        public IEnumerable<Host> getAllHosts()
        {
            return dal.getAllHosts();
        }
        public IEnumerable<HostingUnit> getUnitsForHost(Host host)
        {
            return dal.getAllUnits().Where(unit => unit.OwnerHostKey == host.HostKey);
        }
        public IEnumerable<Order> getOrdersByUnitKey(int unitKey)
        {
            return dal.getAllOrder().Where(order => order.HostingUnitKey == unitKey);
        }
        public IEnumerable<BankBranch> getAllBankBranch()
        {
            //return new List<BankBranch>()
            //{
            //    new BankBranch()
            //    {
            //        BankName="hapoalim",
            //        BankNumber=12,
            //        BranchAddress="pisgat zeev",
            //        BranchCity="jerozalem",
            //        BranchNumber=669
            //    },
            //    new BankBranch()
            //    {
            //        BankName="discont",
            //        BankNumber=13,
            //        BranchAddress="givat zeev",
            //        BranchCity="jerozalem",
            //        BranchNumber=679
            //    }
            //};
            return dal.getAllBranches();
        }
    }

}
