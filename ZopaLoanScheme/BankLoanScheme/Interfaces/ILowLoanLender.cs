using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;

namespace BankLoanScheme.Interfaces
{
    public interface ILowLoanLender
    {
        decimal ComputeLenderContributaryLoan(decimal amountToLoan, LenderData lenderData);
        IList<LenderData> GetLenderContributors(decimal totalAmountToLoan, IList<LenderData> lendersData);
    }
}
