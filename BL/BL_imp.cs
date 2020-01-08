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
        public MyDate today()
        {
            return new MyDate(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
        }
        public IEnumerable<HostingUnit> getEmptyUnits(MyDate date, int numOfDays)
        {
            MyDate last = new MyDate(date);
            last.addDays(numOfDays);
            return dal.getAllUnits().Where(unit => unit.checkEmpty(date,last));
        }

        public int getNumOfDays(MyDate first, MyDate last)
        {
            MyDate temp = new MyDate(first);
            int i;
            if (last.CompareTo(first) <= 0)//if last<=first
                throw new NoDaysException();
            for (i = 0; !(temp == last); i++,temp.addDays(1)) ;
            return i;
        }

        public int getNumOfDays(MyDate date)
        {
            MyDate last = today();
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
            return dal.getAllOrder().Where(order => getNumOfDays(order.CreateDate, today()) >= days);
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
            return dal.getAllHost().GroupBy(host => getNumOfUnits(host));
        }

        public int getNumOfUnits(Host host)
        {
            return dal.getAllUnits().Count(unit => unit.Owner.HostKey == host.HostKey);
        }

        public IEnumerable<IEnumerable<GuestRequest>> groupReqByArea()
        {
            return dal.getAllGuestRequest().GroupBy(req => req.Area);
        }

        public IEnumerable<IEnumerable<HostingUnit>> groupUnitsByArea()
        {
            return dal.getAllUnits().GroupBy(unit => unit.Area);
        }

        public int acceptOrder(Order order)
        {
            if (dal.getAllOrder().FirstOrDefault(item => item.OrderKey == order.OrderKey).Status == OrderStatus.MailSent)
            {
                GuestRequest req = getRequest(order);
                updateOrderStatus(order);
                getHostingUnit(order).updateDiary(req.EntryDate, req.ReleaseDate);
                req.ActiveStatus = false;
                dal.updateRequest(req);
                return calculateCommission(req);
            }
            return 0;
        }
        
        public GuestRequest getRequest(Order order)
        {
            return dal.getAllGuestRequest().FirstOrDefault(req => order.GuestRequestKey == req.GuestRequestKey);
        }
        public HostingUnit getHostingUnit(Order order)
        {
            return dal.getAllUnits().FirstOrDefault(unit => order.HostingUnitKey == unit.HostingUnitKey);
        }

        public int calculateCommission(GuestRequest req)
        {
            return getNumOfDays(req.EntryDate, req.ReleaseDate) * Configuration.commission;
        }
        public void rejectOrder(Order order)
        {
            if (dal.getAllOrder().FirstOrDefault(item => item.OrderKey == order.OrderKey).Status == OrderStatus.MailSent)
            {
                order.Status = OrderStatus.NoResponsCustomerClose;
                dal.updateOrder(order);
            }
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
            return dal.getAllGuestRequest.Where(req => unit.fitCheck(req));
        }
        public void addRequest(GuestRequest req)
        {

        }
    }
    
    
}
