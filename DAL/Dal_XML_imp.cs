using System;
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
        private string hostsPath = @"HostsXml.xml";
        public Dal_XML_imp()
        {
            if (!File.Exists(hostsPath))
                CreateFileHosts();
            loadHosts();
        }
        private void loadHosts()
        {
            hostsRoot = XElement.Load(hostsPath);
        }
        private void CreateFileHosts()
        {
            hostsRoot = new XElement("hosts");
            hostsRoot.Save(hostsPath);
        }

        public void addHost(Host host)
        {
            if (!IsExistHost(host.HostKey))
                throw new DuplicateIdException("host", host.HostKey);
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
            throw new NotImplementedException();
        }

        public void addOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void addRequest(GuestRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankBranch> getAllBranches()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> getAllGuestRequest()
        {
            throw new NotImplementedException();
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
                            BankNumber = Int32.Parse(temp.Element("BankNumber").Value),
                            BankName = temp.Element("BankName").Value,
                            BranchNumber = Int32.Parse(temp.Element("BranchNumber").Value),
                            BranchAddress = temp.Element("BranchAddress").Value,
                            BranchCity = temp.Element("BranchCity").Value
                        },
                        BankAccountNumber = Int32.Parse(temp.Element("BankAccountNumber").Value),
                        CollectionClearance = bool.Parse(temp.Element("CollectionClearance").Value),
                        Password = temp.Element("Password").Value
                    }
                   );
            return list;
        }

        public IEnumerable<Order> getAllOrder()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> getAllUnits()
        {
            throw new NotImplementedException();
        }

        public void removeHostingUnit(int id)
        {
            throw new NotImplementedException();
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

        public void updateHostingUnit(HostingUnit unit)
        {
            throw new NotImplementedException();
        }

        public void updateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void updateRequest(GuestRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
