using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoanScheme.Domain
{
    public class LenderData
    {
        public string Lender { get; set; }
        public double Rate { get; set; }
        public decimal Available { get; set; }
        public decimal AmountLent { get; set; }
    }
}
