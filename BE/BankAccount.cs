using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class BankAccount
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public int BankAccountNumber { get; set; }
        public override string ToString()
        {
            return base.ToString();//
        }
    }
}
