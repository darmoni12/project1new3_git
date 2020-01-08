using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        public static List<GuestRequest> requestsList = new List<GuestRequest>()
        {
            new GuestRequest()
            {
                FamilyName="Levi",
                PrivateName="yacov",
                MailAddress="yacovlevi8@gmail.com",
                GuestRequestKey=10000001,
                Status=OrderStatus.Not_Treated,
                Adults=2,
                Children=2,
                Area =Area.Center,
                SubArea= "Jerusalem",
                ChildrensAttractions =Require.Possible,
                Garden=Require.Possible,
                Jacuzzi= Require.Necessary,
                Pool=Require.Necessary,
                Type=HostingType.Hotel,
            },
            new GuestRequest()
            {
                FamilyName="darmony",
                PrivateName="idan",
                MailAddress="1999darmoni@gmail.com",
                GuestRequestKey=10000002,
                Status=OrderStatus.Not_Treated,
                Adults=2,
                Children=2,
                Area =Area.Center,
                SubArea= "Jerusalem",
                ChildrensAttractions =Require.Possible,
                Garden=Require.Possible,
                Jacuzzi= Require.Necessary,
                Pool=Require.Necessary,
                Type=HostingType.Hotel,
            },
            new GuestRequest()
            {
                FamilyName="le",
                PrivateName="chang",
                MailAddress="chang@gmail.com",
                GuestRequestKey=10000003,
                Status=OrderStatus.Not_Treated,
                Adults=2,
                Children=2,
                Area =Area.Center,
                SubArea= "Jerusalem",
                ChildrensAttractions =Require.Possible,
                Garden=Require.Possible,
                Jacuzzi= Require.Necessary,
                Pool=Require.Necessary,
                Type=HostingType.Hotel,
            }
        };

        public static List<Order> ordersList = new List<Order>()
        {
            new Order()
            {
                OrderKey=1,
                GuestRequestKey=11,
                HostingUnitKey=2222,
                CreateDate =new MyDate(1,1,2020),
                OrderDate = new MyDate(29,12,2019),
                Status= OrderStatus.MailSent
            },
            new Order()
            {
                OrderKey=2,
                GuestRequestKey=22,
                HostingUnitKey=3,
                CreateDate =new MyDate(1,1,2020),
                OrderDate = new MyDate(18,12,2019),
                Status= OrderStatus.MailSent
            },
            new Order()
            {
                OrderKey=4,
                GuestRequestKey=5,
                HostingUnitKey=2222,
                CreateDate =new MyDate(1,1,2020),
                OrderDate = new MyDate(18,12,2019),
                Status= OrderStatus.MailSent
            }
        };
        public static List<HostingUnit> hostingUnitsList = new List<HostingUnit>()
        {
            new HostingUnit()
            {
                HostingUnitKey=5555,
                HostingUnitName="adfsdsdg",
                Owner=new Host()
                {
                    CollectionClearance=Aproved.Yes,
                    FamilyName="sss",
                    PrivateName="mendi",
                    PhoneNumber="0524408400",
                    MailAddress="asssss@mail.com",
                    BankAccountNumber=9654,
                    BankBranchDetails=new BankBranch()
                    {
                        
                    }
                    ,
                    HostKey=55555
                }
            
            }
        };
        public static List<Host> hostList = new List<Host>();
    }
}
