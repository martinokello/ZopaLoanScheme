using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;

namespace BankLoanScheme.Interfaces
{
    public interface IRepayment
    {
        decimal ComputeAmountRepayable(LenderData lenderData);
        decimal ComputeMonthlyRepaymentAmount(IList<LenderData> lenderData, int inNumberOfMonths);
        decimal ComputeTotalRepaymentAmount(IList<LenderData> lenderData);
        double ComputeTotalRateOfRepayment(IList<LenderData> lenderData);
        double RoundToTheNearest(double value, int decimalPlaces);
    }
}
