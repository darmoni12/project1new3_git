using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        IEnumerable<HostingUnit> getEmptyUnits(MyDate date, int numOfDays);
        int getNumOfDays(MyDate first, MyDate last);
        int getNumOfDays(MyDate date);
        IEnumerable<Order> getOrders(int days);
        IEnumerable<GuestRequest> getRequestIf(Func<GuestRequest,bool> predicate);
        int getNumOfOrder(GuestRequest req);
        int getNumOfOrder(HostingUnit unit);

        IEnumerable<IEnumerable<GuestRequest>> groupReqByArea();
        IEnumerable<IEnumerable<GuestRequest>> groupByNumOfPeople();
        IEnumerable<IEnumerable<Host>> groupByNumOfUnits();
        IEnumerable<IEnumerable<HostingUnit>> groupUnitsByArea(); 
    }
}
