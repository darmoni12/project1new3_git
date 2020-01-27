using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    [Serializable]
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance;//אישור חיוב
        public string Password { get; set; }
        public override string ToString()
        {
            return HostKey + ": " + PrivateName + " " + FamilyName;
        }
        public bool CheckPassword(string pass)
        {
            if (Password == pass)
                return true;
            return false;
        }
    }
}
