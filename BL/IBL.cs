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

        void addRequest(GuestRequest req);
        IEnumerable<HostingUnit> getAllUnits();
        void removeHostingUnit(int id);
        void updateHostingUnit(HostingUnit unit);
        IEnumerable<Host> getAllHosts();

        void addHostingUnit(HostingUnit unit);
        Host getHost(int hostKey);


        void addHost(Host host);
        void updateHost(Host host);
        IEnumerable<HostingUnit> getUnitsForHost(Host host);
        IEnumerable<Order> getOrdersByUnitKey(int unitKey);
        void makeOrder(int requestKey, int unitKey);
        bool isInOrderList(GuestRequest request, IEnumerable<Order> orderList);
        int acceptOrder(Order order);
        void rejectOrderSp(Order order);
        List<BankBranch> getAllBankBranch();







    }
}
