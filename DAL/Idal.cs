using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface Idal
    {
        void addRequest(GuestRequest request);
        void updateRequest(GuestRequest request);

        void addHostingUnit(HostingUnit unit);
        void deleteHostingUnit(int id);
        void updateHostingUnit(HostingUnit unit);

        void addOrder(Order order);
        void updateOrder(Order order);

        IEnumerable<HostingUnit> getAllUnits();
        IEnumerable<GuestRequest> getAllGuestRequest();
        IEnumerable<Order> getAllOrder();
        IEnumerable<Host> getAllHost();
        List<BankBranch> getAllBranches();
    }
}