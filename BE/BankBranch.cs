using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class BankBranch
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }

        public override string ToString()
        {
            return BankName + " " + BankNumber + " BranchNumber: " + BranchNumber;
        }
    }
}
