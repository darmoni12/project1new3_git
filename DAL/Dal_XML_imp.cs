using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using BE;
using System.ComponentModel;
using System.Threading;
using System.Net;

namespace DAL
{
    class Dal_XML_imp : Idal
    {
        private XElement hostsRoot;
        private XElement atmsRoot;
        private XElement configurationRoot;
        private const string hostsPath = @"HostsXml.xml";
        private const string unitsPath = @"UnitsXml.xml";
        private const string requestsPath = @"RequestsXml.xml";
        private const string ordersPath = @"OrdersXml.xml";
        private const string atmsPath = @"atmsXml.xml";
        private const string configurationPath = @"configurationXml.xml";
        public Dal_XML_imp()
        {
            if (!File.Exists(configurationPath))
                CreateFileConfiguration();
            loadConfiguration();
                
            if (!File.Exists(hostsPath))
                CreateFileHosts();
            loadHosts();
            if (!File.Exists(unitsPath))
            {
                List<HostingUnit> units = new List<HostingUnit>();
                SaveToXML<List<HostingUnit>>(units, unitsPath);
            }
            if (!File.Exists(requestsPath))
            {
                List<GuestRequest> requests = new List<GuestRequest>();
                SaveToXML<List<GuestRequest>>(requests, requestsPath);
            }
            if (!File.Exists(ordersPath))
            {
                List<Order> orders = new List<Order>();
                SaveToXML<List<Order>>(orders, ordersPath);
            }
            BackgroundWorker atmworker = new BackgroundWorker();
            atmworker.DoWork += atmload;
            //atmworker.ProgressChanged += Worker_ProgressChanged;
            atmworker.WorkerReportsProgress = true;
            atmworker.RunWorkerAsync();
        }
        private void CreateFileConfiguration()
        {
            configurationRoot = new XElement("configuration");
            configurationRoot.Add(new XElement("GuestRequestSerialNum", Configuration.GuestRequestSerialNum-1));
            configurationRoot.Add(new XElement("OrderSerialNum", Configuration.OrderSerialNum-1));
            configurationRoot.Add(new XElement("HostingUnitSerialNum", Configuration.HostingUnitSerialNum-1));
            configurationRoot.Add(new XElement("HostSerialNum", Configuration.HostSerialNum-1));
            configurationRoot.Add(new XElement("SumOfCommission","0"));
            configurationRoot.Save(configurationPath);
        }
        private void atmload(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    WebClient wc = new WebClient();
                    try
                    {
                        //string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                        //wc.DownloadFile(xmlServerPath, atmsPath);
                        string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                        wc.DownloadFile(xmlServerPath, atmsPath);
                    }
                    catch (Exception)
                    {
                        string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                        wc.DownloadFile(xmlServerPath, atmsPath);
                    }
                    finally
                    {
                        wc.Dispose();
                        atmsRoot = XElement.Load(atmsPath);
                    }
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                }
            }
        }
        private void loadConfiguration()
        {
            configurationRoot = XElement.Load(configurationPath);
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
        //private void CreateFileUnits()
        //{
        //    unitsRoot = new XElement("units");
        //    unitsRoot.Save(unitsPath);
        //}
        //private void CreateFileRequests()
        //{
        //    requestsRoot = new XElement("requests");
        //    requestsRoot.Save(requestsPath);
        //}
        //private void CreateFileOrders()
        //{
        //    ordersRoot = new XElement("orders");
        //    ordersRoot.Save(ordersPath);
        //}
        public void addHost(Host host)
        {
            if (IsExistHost(host.HostKey))
                throw new DuplicateIdException("host", host.HostKey);
            host.HostKey = Int32.Parse(configurationRoot.Element("HostSerialNum").Value) + 1;
            configurationRoot.Element("HostSerialNum").Value= host.HostKey.ToString();
            configurationRoot.Save(configurationPath);
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
            unit.HostingUnitKey = Int32.Parse(configurationRoot.Element("HostingUnitSerialNum").Value) + 1;
            configurationRoot.Element("HostingUnitSerialNum").Value = unit.HostingUnitKey.ToString();
            configurationRoot.Save(configurationPath);
            List<HostingUnit> unitslist = LoadFromXML<List<HostingUnit>>(unitsPath);
            unitslist.Add(unit);
            SaveToXML(unitslist, unitsPath);
        }

        public void addOrder(Order order)
        {
            if (IsExistOrder(order.OrderKey))
                throw new DuplicateIdException("order", order.OrderKey);
            order.OrderKey = Int32.Parse(configurationRoot.Element("OrderSerialNum").Value) + 1;
            configurationRoot.Element("OrderSerialNum").Value = order.OrderKey.ToString();
            configurationRoot.Save(configurationPath);
            List<Order> orderslist = LoadFromXML<List<Order>>(ordersPath);
            orderslist.Add(order);
            SaveToXML(orderslist, ordersPath);
        }

        public void addRequest(GuestRequest request)
        {
            if (IsExistRequest(request.GuestRequestKey))
                throw new DuplicateIdException("request", request.GuestRequestKey);
            request.GuestRequestKey = Int32.Parse(configurationRoot.Element("GuestRequestSerialNum").Value) + 1;
            configurationRoot.Element("GuestRequestSerialNum").Value = request.GuestRequestKey.ToString();
            configurationRoot.Save(configurationPath);
            List<GuestRequest> list = LoadFromXML<List<GuestRequest>>(requestsPath);
            list.Add(request);
            SaveToXML(list, requestsPath);
        }

        public IEnumerable<BankBranch> getAllBranches()
        {
            IEnumerable<BankBranch> list = new List<BankBranch>();
            list = (from temp in atmsRoot.Elements()
                    select new BankBranch()
                    {
                        BankNumber = Int32.Parse(temp.Element("קוד_בנק").Value),
                        BankName = temp.Element("שם_בנק").Value,
                        BranchNumber = Int32.Parse(temp.Element("קוד_סניף").Value),
                        BranchAddress = temp.Element("כתובת_ה-ATM").Value,
                        BranchCity = temp.Element("ישוב").Value
                    }
                   ).ToList();
            return list;
        }

        public IEnumerable<GuestRequest> getAllGuestRequest()
        {
            return LoadFromXML<List<GuestRequest>>(requestsPath);
        }

        public IEnumerable<Host> getAllHosts()
        {
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
            return LoadFromXML<List<Order>>(ordersPath);
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
            if (!IsExistHost(host.HostKey))
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
            List<HostingUnit> units = LoadFromXML<List<HostingUnit>>(unitsPath);
            return units.Any(unit => unit.HostingUnitKey == id);
        }
        private bool IsExistRequest(int id)
        {
            List<GuestRequest> requests = LoadFromXML<List<GuestRequest>>(requestsPath);
            return requests.Any(request => request.GuestRequestKey == id);
        }
        private bool IsExistOrder(int id)
        {
            List<Order> orders = LoadFromXML<List<Order>>(ordersPath);
            return orders.Any(order => order.OrderKey == id);
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
            FileStream file = new FileStream(path, FileMode.Create,FileAccess.Write);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open,FileAccess.Read);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
        public void updateCommision(int comis)
        {
            int sum = int.Parse( configurationRoot.Element("SumOfCommission").Value);
            sum += comis;
            configurationRoot.Element("SumOfCommission").Value = sum.ToString();
            configurationRoot.Save(configurationPath);
        }
        public int getSumOfCommission()
        {
            return int.Parse(configurationRoot.Element("SumOfCommission").Value);
        }
    }
}
