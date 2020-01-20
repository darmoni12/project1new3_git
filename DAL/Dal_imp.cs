using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DS;
using System.Linq;

namespace DAL
{
    public class Dal_imp : Idal
    {
        public bool CheckHostingUnit(int key)
        {
            return DataSource.hostingUnitsList.Any(unit => unit.HostingUnitKey == key);
        }
        public void addHostingUnit(HostingUnit unit)
        {
            if (CheckHostingUnit(unit.HostingUnitKey))
                throw new DuplicateIdException("hotingunit", unit.HostingUnitKey);
            unit.HostingUnitKey = Configuration.HostingUnitSerialNum;
            DataSource.hostingUnitsList.Add(Cloning.Clone(unit));
        }

        public bool CheckOrder(int id)
        {
            return DataSource.ordersList.Any(order=>order.OrderKey == id);
        }
        public void addOrder(Order order)
        {
            if (CheckOrder(order.OrderKey))
                throw new DuplicateIdException("order", order.OrderKey);
            DataSource.ordersList.Add(Cloning.Clone(order));
        }

        public bool CheckRequest(int id)
        {
            return DataSource.requestsList.Any(req => req.GuestRequestKey == id);
        }
        public void addRequest(GuestRequest request)
        {
            if (CheckOrder(request.GuestRequestKey))
                throw new DuplicateIdException("request", request.GuestRequestKey);
            request.GuestRequestKey = Configuration.GuestRequestSerialNum;
            DataSource.requestsList.Add(Cloning.Clone(request));
        }

        public void removeHostingUnit(int id)
        {
            int count = DataSource.hostingUnitsList.RemoveAll(item=>item.HostingUnitKey == id);
            if (count == 0)
                throw new MissingIdException("hostingUnit", id);
        }

        public List<BankBranch> getAllBranches()
        {
            return new List<BankBranch>()
            {
               new BankBranch()
               {
                   BankName="rgh",BankNumber=54,BranchAddress="rsdf",BranchCity="wsfgd",BranchNumber=3422
               },
               new BankBranch()
               {
                   BranchNumber=54,BranchCity="sgsdbf",BranchAddress="sfg",BankNumber=987,BankName="dgs"
               },
               new BankBranch()
               {
                   BranchNumber=87,BranchCity="yjfgf",BranchAddress="sufhg",BankNumber=977,BankName="jyfs"
               },
                new BankBranch()
               {
                   BranchNumber=91,BranchCity="yt",BranchAddress="iuh",BankNumber=125,BankName="jhgs"
               }, new BankBranch()
               {
                   BranchNumber=84,BranchCity="drt",BranchAddress="bn",BankNumber=957,BankName="jhgfs"
               }
            };
        }

        public IEnumerable<GuestRequest> getAllGuestRequest()
        {
            return from request in DataSource.requestsList
                   select Cloning.Clone(request);
        }

        public IEnumerable<Order> getAllOrder()
        {
            return from order in DataSource.ordersList
                   select Cloning.Clone(order);
        }

        public IEnumerable<HostingUnit> getAllUnits()
        {
            return from unit in DataSource.hostingUnitsList
                   select Cloning.Clone(unit);
        }

        public IEnumerable<Host> getAllHosts()
        {
            return from host in DataSource.hostList
                   select Cloning.Clone(host);
        }

        public void updateHostingUnit(HostingUnit unit)
        {
            int count = DataSource.hostingUnitsList.RemoveAll(item =>item.HostingUnitKey == unit.HostingUnitKey);
            if (count == 0)
                throw new MissingIdException("hostingUnit", unit.HostingUnitKey);
            if (CheckHostingUnit(unit.HostingUnitKey))
                throw new DuplicateIdException("hotingUnit", unit.HostingUnitKey);
            DataSource.hostingUnitsList.Add(Cloning.Clone(unit));
        }

        public void updateOrder(Order order)
        {
            int count = DataSource.ordersList.RemoveAll(item => item.OrderKey == order.OrderKey);
            if (count == 0)
                throw new MissingIdException("order", order.OrderKey);
            addOrder(order);
        }

        public void updateRequest(GuestRequest request)
        {
            int count = DataSource.requestsList.RemoveAll(item => item.GuestRequestKey == request.GuestRequestKey);
            if (count == 0)
                throw new MissingIdException("request", request.GuestRequestKey);
            addRequest(request);
        }
    }
}
