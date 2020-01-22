using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BE
{
    public static class Cloning
    {
        public static BankAccount Clone(this BankAccount original)
        {
            BankAccount target = new BankAccount();
            target.BankNumber = original.BankNumber;
            target.BankName = original.BankName;
            target.BranchNumber = original.BranchNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            target.BankAccountNumber = original.BankAccountNumber;
            return target;
        }
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();
            target.RegistrationDate = Clone(original.RegistrationDate);
            target.ReleaseDate = Clone(original.ReleaseDate);
            target.EntryDate = Clone(original.EntryDate);
            target.Adults = original.Adults;
            target.Area = original.Area;
            target.Children = original.Children;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.FamilyName = original.FamilyName;
            target.Garden = original.Garden;
            target.GuestRequestKey = original.GuestRequestKey;
            target.Jacuzzi = original.Jacuzzi;
            target.MailAddress = original.MailAddress;
            target.Pool = original.Pool;
            target.PrivateName = original.PrivateName;
            target.ActiveStatus = original.ActiveStatus;
            target.Type = original.Type;
            target.Food = original.Food;
            target.FreeParking = original.FreeParking;
            target.FreeWifi = original.FreeWifi;
            return target;
        }
        public static MyDate Clone(this MyDate original)
        {
            MyDate target = new MyDate();
            target.Day = original.Day;
            target.Month = original.Month;
            target.Year = original.Year;
            return target;
        }
        public static Diary Clone(this Diary original)
        {
            Diary target = new Diary();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                    target.diary[i, j] = original.diary[i, j];
            }
            return target;
        }
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.HostingUnitKey = original.HostingUnitKey;
            target.OwnerHostKey = original.OwnerHostKey;
            target.HostingUnitName = original.HostingUnitName;
            target.Diary = Clone(original.Diary);
            target.Area = original.Area;
            target.Address = original.Address;
            target.Type = original.Type;
            target.MaxPeople = original.MaxPeople;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.Breakfast = original.Breakfast;
            target.HB = original.HB;
            target.FB = original.FB;
            target.BedOnly = original.BedOnly;
            target.FreeWifi = original.FreeWifi;
            target.FreeParking = original.FreeParking;
            return target;
        }
        public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.CreateDate = Clone(original.CreateDate);
            target.GuestRequestKey = original.GuestRequestKey;
            target.HostingUnitKey = original.HostingUnitKey;
            target.OrderKey = original.OrderKey;
            target.Status = original.Status;
            return target;
        }
        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.HostKey = original.HostKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.PhoneNumber = original.PhoneNumber;
            target.MailAddress = original.MailAddress;
            target.BankBranchDetails = Clone(original.BankBranchDetails);
            target.BankAccountNumber = original.BankAccountNumber;
            target.CollectionClearance = original.CollectionClearance;
            target.Password = original.Password;
            return target;
        }
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.BankNumber = original.BankNumber;
            target.BankName = original.BankName;
            target.BranchNumber = original.BankNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            return target;
        }

    }
}
