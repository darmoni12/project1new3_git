using System;

namespace BE
{
    public class GuestRequest
    {
        public int GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public bool ActiveStatus;
        public MyDate RegistrationDate { get; set; }
        public MyDate EntryDate { get; set; }
        public MyDate ReleaseDate { get; set; }
        public Area Area;
        public HostingType Type;
        public int Adults { get; set; }
        public int Children { get; set; }
        public Require Pool;
        public Require Jacuzzi;
        public Require Garden;
        public Require ChildrensAttractions;
        public Food Food;
        public Require FreeWifi;
        public Require FreeParking;

        public override string ToString()
        {
            return base.ToString();//not imlemented yet
        }
    }
}
