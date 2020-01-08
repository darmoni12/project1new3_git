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
                RegistrationDate=new MyDate(1,1,2020) ,                   
                EntryDate=new MyDate(2,1,2020),
                ReleaseDate=new MyDate(5,1,2020),
                
                ActiveStatus=true,
                Adults=2,
                Children=2,
                Area =Area.Center,
                ChildrensAttractions =Require.Possible,
                Garden=Require.Possible,
                Jacuzzi= Require.Necessary,
                Pool=Require.Necessary,
                Type=HostingType.Hotel,
                FreeParking=Require.Possible,
                FreeWifi = Require.Possible,
                Food=Food.Breakfast,
            },
            new GuestRequest()
            {
                FamilyName="darmony",
                PrivateName="idan",
                MailAddress="1999darmoni@gmail.com",
                GuestRequestKey=10000002,
                RegistrationDate=new MyDate(1,1,2020) ,
                EntryDate=new MyDate(8,1,2020),
                ReleaseDate=new MyDate(13,1,2020),

                ActiveStatus=true,
                Adults=2,
                Children=2,
                Area =Area.Center,
                ChildrensAttractions =Require.Possible,
                Garden=Require.Possible,
                Jacuzzi= Require.Necessary,
                Pool=Require.Necessary,
                Type=HostingType.Hotel,
                FreeParking=Require.Possible,
                FreeWifi = Require.Possible,
                Food =Food.HB,
                
            },
            new GuestRequest()
            {
                FamilyName="le",
                PrivateName="chang",
                MailAddress="chang@gmail.com",
                GuestRequestKey=10000003,
                RegistrationDate=new MyDate(12,2,2020) ,
                EntryDate=new MyDate(20,2,2020),
                ReleaseDate=new MyDate(25,2,2020),

                ActiveStatus =true,
                Adults=2,
                Children=2,
                Area =Area.Center,
                ChildrensAttractions =Require.Possible,
                Garden=Require.Possible,
                Jacuzzi= Require.Necessary,
                Pool=Require.Necessary,
                Type=HostingType.Hotel,
                FreeParking=Require.Possible,
                FreeWifi = Require.Possible,
                Food =Food.HB,
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
                Status= OrderStatus.MailSent
            },
            new Order()
            {
                OrderKey=2,
                GuestRequestKey=22,
                HostingUnitKey=3,
                CreateDate =new MyDate(1,1,2020),
                Status= OrderStatus.MailSent
            },
            new Order()
            {
                OrderKey=4,
                GuestRequestKey=5,
                HostingUnitKey=2222,
                CreateDate =new MyDate(1,1,2020),
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
                    CollectionClearance=true,
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

                },
                FB=true,
                HB=true,
                BedOnly=true,
                Breakfast=true,
                Address="asdasdasd",
                Area=Area.Center,
                Type=HostingType.Hotel,
                FreeParking=true,
                FreeWifi=true,
                Jacuzzi=false,
                Pool=true,
                Garden=true,
                ChildrensAttractions=true,
                Diary=new Diary(),
                MaxPeople=8,
 
            },
            new HostingUnit()
            {
                HostingUnitKey=22222,
                HostingUnitName="darmony unit",
                Owner=new Host()
                {
                    CollectionClearance=true,
                    FamilyName="darmony",
                    PrivateName="idan",
                    PhoneNumber="0525555555",
                    MailAddress="dardar@mail.com",
                    BankAccountNumber=9654,
                    BankBranchDetails=new BankBranch()
                    {

                    }
                    ,
                    HostKey=22

                },
                FB=true,
                HB=true,
                BedOnly=true,
                Breakfast=true,
                Address="pisga",
                Area=Area.Center,
                Type=HostingType.Hotel,
                FreeParking=true,
                FreeWifi=true,
                Jacuzzi=false,
                Pool=true,
                Garden=true,
                ChildrensAttractions=true,
                Diary=new Diary(),
                MaxPeople=8,

            }
        };
        public static List<Host> hostList = new List<Host>();
    }
}
