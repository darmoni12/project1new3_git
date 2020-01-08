using System;

namespace BE
{
    public class GuestRequest
    {
        public int GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public OrderStatus Status;
        internal MyDate RegistrationDate { get; set; }
        internal MyDate EntryDate { get; set; }
        internal MyDate ReleaseDate { get; set; }
        public Area Area;
        public string SubArea { get; set; }
        public HostingType Type;
        public int Adults { get; set; }
        public int Children { get; set; }
        public Require Pool;
        public Require Jacuzzi;
        public Require Garden;
        public Require ChildrensAttractions;
        public int NumOfOrders { get; set; }
        public override string ToString()
        {
            return base.ToString();//not imlemented yet
        }
    }
}
