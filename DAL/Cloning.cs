using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
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
            target.Status = original.Status;
            target.SubArea = original.SubArea;
            target.Type = original.Type;
            return target;
        }
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.HostingUnitKey = original.HostingUnitKey;
            target.Owner = original.Owner;
            target.HostingUnitName = original.HostingUnitName;
            target.Diary = new Diary(original.Diary);
            return target;
        }
       public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.CreateDate = new MyDate(original.CreateDate);
            target.GuestRequestKey = original.GuestRequestKey;
            target.HostingUnitKey = original.HostingUnitKey;
            target.OrderDate = new MyDate(original.OrderDate);
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
