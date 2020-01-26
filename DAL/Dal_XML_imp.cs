﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using BE;

namespace DAL
{
    class Dal_XML_imp : Idal
    {
        private XElement hostsRoot;
        private XElement unitsRoot;
        private XElement requestsRoot;
        private XElement ordersRoot;
        private string hostsPath = @"HostsXml.xml";
        private string unitsPath = @"UnitsXml.xml";
        private string requestsPath = @"RequestsXml.xml";
        private string ordersPath = @"OrdersXml.xml";
        public Dal_XML_imp()
        {
            if (!File.Exists(hostsPath))
                CreateFileHosts();
            loadHosts();
            if (!File.Exists(unitsPath))
                CreateFileUnits();
            loadUnits();
            if (!File.Exists(requestsPath))
                CreateFileRequests();
            loadRequests();
            if (!File.Exists(ordersPath))
                CreateFileOrders();
            loadOrders();
        }
        private void loadHosts()
        {
            hostsRoot = XElement.Load(hostsPath);
        }
        private void loadUnits()
        {
            unitsRoot = XElement.Load(unitsPath);
        }
        private void loadRequests()
        {
            requestsRoot = XElement.Load(requestsPath);
        }
        private void loadOrders()
        {
            ordersRoot = XElement.Load(ordersPath);
        }
        private void CreateFileHosts()
        {
            hostsRoot = new XElement("hosts");
            hostsRoot.Save(hostsPath);
        }
        private void CreateFileUnits()
        {
            unitsRoot = new XElement("units");
            unitsRoot.Save(unitsPath);
        }
        private void CreateFileRequests()
        {
            requestsRoot = new XElement("requests");
            requestsRoot.Save(requestsPath);
        }
        private void CreateFileOrders()
        {
            ordersRoot = new XElement("orders");
            ordersRoot.Save(ordersPath);
        }
        public void addHost(Host host)
        {
            if (IsExistHost(host.HostKey))
                throw new DuplicateIdException("host", host.HostKey);
            host.HostKey = Configuration.HostSerialNum;
            hostsRoot.Add(new XElement("host",
                                      new XElement("HostKey", host.HostKey),
                                      new XElement("PrivateName", host.PrivateName),
                                      new XElement("FamilyName", host.FamilyName),
                                      new XElement("PhoneNumber", host.PhoneNumber),
                                      new XElement("MailAddress", host.MailAddress),
                                      new XElement("BankBranchDetails",
                                                   new XElement("BankNumber", host.BankBranchDetails.BankNumber),
                                                   new XElement("BankName", host.BankBranchDetails.BankName),
                                                   new XElement("BranchNumber", host.BankBranchDetails.BranchNumber),
                                                   new XElement("BranchAddress", host.BankBranchDetails.BranchAddress),
                                                   new XElement("BranchCity", host.BankBranchDetails.BranchCity)
                                                  ),
                                      new XElement("BankAccountNumber", host.BankAccountNumber),
                                      new XElement("CollectionClearance", host.CollectionClearance),
                                      new XElement("Password", host.Password)
                                     )
                        );
            hostsRoot.Save(hostsPath);
        }

        public void addHostingUnit(HostingUnit unit)
        {
            if (IsExistUnit(unit.HostingUnitKey))
                throw new DuplicateIdException("hosting unit", unit.HostingUnitKey);
            unit.HostingUnitKey = Configuration.HostingUnitSerialNum;
            List<HostingUnit> unitslist = LoadFromXML<List<HostingUnit>>(unitsPath);
            unitslist.Add(unit);
            SaveToXML(unitslist, unitsPath);
        }

        public void addOrder(Order order)
        {
            if (IsExistOrder(order.OrderKey))
                throw new DuplicateIdException("order", order.OrderKey);
            order.OrderKey = Configuration.OrderSerialNum;
            List<Order> orderslist = LoadFromXML<List<Order>>(ordersPath);
            orderslist.Add(order);
            SaveToXML(orderslist, ordersPath);
        }

        public void addRequest(GuestRequest request)
        {
            if (IsExistRequest(request.GuestRequestKey))
                throw new DuplicateIdException("request", request.GuestRequestKey);
            request.GuestRequestKey = Configuration.GuestRequestSerialNum;
            List<GuestRequest> list = LoadFromXML<List<GuestRequest>>(requestsPath);
            list.Add(request);
            SaveToXML(list, requestsPath);
        }

        public IEnumerable<BankBranch> getAllBranches()
        {
            throw new NotImplementedException();/////////////////////////////
        }

        public IEnumerable<GuestRequest> getAllGuestRequest()
        {
            return LoadFromXML<IEnumerable<GuestRequest>>(requestsPath);
        }

        public IEnumerable<Host> getAllHosts()
        {
            loadHosts();
            IEnumerable<Host> list = new List<Host>();
            list = (from temp in hostsRoot.Elements()
                    select new Host()
                    {
                        HostKey = Int32.Parse(temp.Element("HostKey").Value),
                        PrivateName = temp.Element("PrivateName").Value,
                        FamilyName = temp.Element("FamilyName").Value,
                        PhoneNumber = temp.Element("PhoneNumber").Value,
                        MailAddress = temp.Element("MailAddress").Value,
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = Int32.Parse(temp.Element("BankBranchDetails").Element("BankNumber").Value),
                            BankName = temp.Element("BankBranchDetails").Element("BankName").Value,
                            BranchNumber = Int32.Parse(temp.Element("BankBranchDetails").Element("BranchNumber").Value),
                            BranchAddress = temp.Element("BankBranchDetails").Element("BranchAddress").Value,
                            BranchCity = temp.Element("BankBranchDetails").Element("BranchCity").Value
                        },
                        BankAccountNumber = Int32.Parse(temp.Element("BankAccountNumber").Value),
                        CollectionClearance = bool.Parse(temp.Element("CollectionClearance").Value),
                        Password = temp.Element("Password").Value
                    }
                   ).ToList();
            return list;
        }

        public IEnumerable<Order> getAllOrder()
        {
            return LoadFromXML<IEnumerable<Order>>(ordersPath);
        }

        public IEnumerable<HostingUnit> getAllUnits()
        {
            return LoadFromXML<List<HostingUnit>>(unitsPath);
        }

        public void removeHostingUnit(int id)
        {
            if (!IsExistUnit(id))
                throw new MissingIdException("hosting unit", id);
            List<HostingUnit> list= LoadFromXML<List<HostingUnit>>(unitsPath);
            list.Remove(list.FirstOrDefault(unit => unit.HostingUnitKey == id));
            SaveToXML(list, unitsPath);
        }

        public void updateHost(Host host)
        {
            if (IsExistHost(host.HostKey))
                throw new MissingIdException("host", host.HostKey);
            XElement tempElement = (from temp in hostsRoot.Elements()
                                    where int.Parse(temp.Element("HostKey").Value) == host.HostKey
                                    select temp).FirstOrDefault();
            tempElement.Element("HostKey").Value = host.HostKey.ToString();
            tempElement.Element("PrivateName").Value = host.PrivateName;
            tempElement.Element("FamilyName").Value = host.FamilyName;
            tempElement.Element("PhoneNumber").Value = host.PhoneNumber;
            tempElement.Element("MailAddress").Value = host.MailAddress;
            tempElement.Element("BankBranchDetails").Element("BankNumber").Value = host.BankBranchDetails.BankNumber.ToString();
            tempElement.Element("BankBranchDetails").Element("BankName").Value = host.BankBranchDetails.BankName;
            tempElement.Element("BankBranchDetails").Element("BranchNumber").Value = host.BankBranchDetails.BranchNumber.ToString();
            tempElement.Element("BankBranchDetails").Element("BranchAddress").Value = host.BankBranchDetails.BranchAddress;
            tempElement.Element("BankBranchDetails").Element("BranchCity").Value = host.BankBranchDetails.BranchCity;
            tempElement.Element("BankAccountNumber").Value = host.BankAccountNumber.ToString();
            tempElement.Element("CollectionClearance").Value = host.CollectionClearance.ToString();
            tempElement.Element("Password").Value = host.Password;

            hostsRoot.Save(hostsPath);
        }
        private bool IsExistHost(int id)
        {
            XElement element = (from temp in hostsRoot.Elements()
                                where int.Parse(temp.Element("HostKey").Value) == id
                                select temp).FirstOrDefault();
            return element != null;
        }
        private bool IsExistUnit(int id)
        {
            XElement element = (from temp in unitsRoot.Elements()
                                where int.Parse(temp.Element("UnitKey").Value) == id
                                select temp).FirstOrDefault();
            return element != null;
        }
        private bool IsExistRequest(int id)
        {
            XElement element = (from temp in requestsRoot.Elements()
                                where int.Parse(temp.Element("RequestKey").Value) == id
                                select temp).FirstOrDefault();
            return element != null;
        }
        private bool IsExistOrder(int id)
        {
            XElement element = (from temp in ordersRoot.Elements()
                                where int.Parse(temp.Element("OrderKey").Value) == id
                                select temp).FirstOrDefault();
            return element != null;
        }
        public void updateHostingUnit(HostingUnit unit)
        {
            if (!IsExistUnit(unit.HostingUnitKey))
                throw new MissingIdException("hosting unit", unit.HostingUnitKey);
            removeHostingUnit(unit.HostingUnitKey);
            List<HostingUnit> unitslist = LoadFromXML<List<HostingUnit>>(unitsPath);
            unitslist.Add(unit);
            SaveToXML(unitslist, unitsPath);
        }

        public void updateOrder(Order order)
        {
            if (!IsExistOrder(order.OrderKey))
                throw new MissingIdException("order", order.OrderKey);
            List<Order> list = LoadFromXML<List<Order>>(ordersPath);
            list.RemoveAll(item => order.OrderKey == item.OrderKey);
            list.Add(order);
            SaveToXML(list,ordersPath);
        }

        public void updateRequest(GuestRequest request)
        {
            if (!IsExistRequest(request.GuestRequestKey))
                throw new MissingIdException("request", request.GuestRequestKey);
            List<GuestRequest> list = LoadFromXML<List<GuestRequest>>(requestsPath);
            list.RemoveAll(item => item.GuestRequestKey == request.GuestRequestKey);
            list.Add(request);
            SaveToXML(list, requestsPath);
        }
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
    }
}
