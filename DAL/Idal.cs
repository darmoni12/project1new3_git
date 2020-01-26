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
        void removeHostingUnit(int id);
        void updateHostingUnit(HostingUnit unit);

        void addOrder(Order order);
        void addHost(Host host);

        void updateOrder(Order order);
        void updateHost(Host host);


        IEnumerable<HostingUnit> getAllUnits();
        IEnumerable<GuestRequest> getAllGuestRequest();
        IEnumerable<Order> getAllOrder();
        IEnumerable<Host> getAllHosts();
        IEnumerable<BankBranch> getAllBranches();
    }
}