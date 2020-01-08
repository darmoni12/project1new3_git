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
            return dal.getAllUnits().Where(unit => unit.checkEmpty(date, numOfDays));
        }

        public int getNumOfDays(MyDate first, MyDate last)
        {
            MyDate temp = new MyDate(first);
            int i;
            if (last.CompareTo(first) <= 0)
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
            return req.NumOfOrders;
        }

        public int getNumOfOrder(HostingUnit unit)
        {
            return unit.NumOfOrders;
        }

        public IEnumerable<Order> getOrders(int days)
        {
            return dal.getAllOrder().Where(order => getNumOfDays(order.CreateDate, today()) >= days || (getNumOfDays(order.SentMail, today()) >= days));
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
            return dal.getAllUnits().Count(unit => unit.Owner == host);
        }

        public IEnumerable<IEnumerable<GuestRequest>> groupReqByArea()
        {
            return dal.getAllGuestRequest().GroupBy(req => req.Area);
        }

        public IEnumerable<IEnumerable<HostingUnit>> groupUnitsByArea()
        {
            return dal.getAllUnits().GroupBy(unit => unit.Area);
        }
        public void updateOrderStatus(Order order,OrderStatus status)
        {
            if (dal.getAllOrder().FirstOrDefault(item => item.OrderKey == order.OrderKey).Status == OrderStatus.done)
                return;
            if(status==OrderStatus.done)
            {
                getNumOfDays(order.)
            }
        }
    }

    
}
