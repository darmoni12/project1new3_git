using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        // public BankAccount HostBankAccount { get; set; }
        public int BankAccountNumber { get; set; }
        public Aproved CollectionClearance;
        public override string ToString()
        {
            return base.ToString();//not imlemented yet
        }
    }
}
