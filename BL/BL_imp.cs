using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BL_imp : IBL
    {
        Idal dal = Dali.GetDal();
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
            for (i = 0; temp.CompareTo(last)==0; i++,temp.addDays(1)) ;
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
            //sendMail(getRequest(requestKey).MailAddress, getHostingUnit(unitKey));
            dal.addOrder(order);

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
            updateOrderStatus(order);
            getHostingUnit(order.HostingUnitKey).updateDiary(req.EntryDate, req.ReleaseDate);
            req.ActiveStatus = false;
            dal.updateRequest(req);
            return calculateCommission(req);
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
                order.Status = OrderStatus.NoResponsCustomerClose;
                dal.updateOrder(order);
            }
        }
        public void rejectOrderSp(Order order)
        {
            order.Status = OrderStatus.NoResponsCustomerClose;
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
            if (dal.getAllOrder().Count(order => order.HostingUnitKey == id && order.Status == OrderStatus.MailSent) == 0)//בודק שאין הזמנות פעילות ליחידת אירוח
                dal.removeHostingUnit(id);
            else
                throw new openOrdersException("remove");
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
        public List<BankBranch> getAllBankBranch()
        {
            return new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankName="hapoalim",
                    BankNumber=12,
                    BranchAddress="pisgat zeev",
                    BranchCity="jerozalem",
                    BranchNumber=669
                },
                new BankBranch()
                {
                    BankName="discont",
                    BankNumber=13,
                    BranchAddress="givat zeev",
                    BranchCity="jerozalem",
                    BranchNumber=679
                }
            };
        }
    }

}
