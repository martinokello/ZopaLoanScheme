using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;
using BankLoanScheme.Interfaces;

namespace BankLoanScheme.Concretes
{
    public class RepaymentService:IRepayment
    {
        private ILowLoanLender _lowLoanLender;
        public RepaymentService(ILowLoanLender lowLoanLender)
        {
            _lowLoanLender = lowLoanLender;
        }
        public decimal ComputeAmountRepayable(LenderData lenderData)
        {
            return (decimal)(((double)lenderData.AmountLent) * lenderData.Rate + (double)lenderData.AmountLent);
        }

        public decimal ComputeTotalRepaymentAmount(IList<LenderData> lenderData)
        {
            var totalRepayment = (decimal)0.0;

            foreach (var data in lenderData)
            {
                totalRepayment += ComputeAmountRepayable(data);
            }
            return totalRepayment;
        }

        public decimal ComputeMonthlyRepaymentAmount(IList<LenderData> lenderData, int inNumberOfMonths)
        {
            return ComputeTotalRepaymentAmount(lenderData) / inNumberOfMonths;
        }
        public double ComputeTotalRateOfRepayment(IList<LenderData> lenderData)
        {
            var totalRepayment = ComputeTotalRepaymentAmount(lenderData);
            var amountBorrowed = (decimal)0.0;
            
            foreach (var data in lenderData)
            {
                amountBorrowed +=data.AmountLent;
            }
            return RoundToTheNearest((double)((totalRepayment - amountBorrowed)/ amountBorrowed) * 100, 2);
        }

        public double RoundToTheNearest(double value, int decimalPlaces)
        {
            int valueMultiplyDivideBy = 0;

            switch (decimalPlaces)
            {
                case 1:
                    valueMultiplyDivideBy = 10;
                    break;
                case 2:
                    valueMultiplyDivideBy = 100;
                    break;
                case 3:
                    valueMultiplyDivideBy = 1000;
                    break;
                default:
                    valueMultiplyDivideBy = 10;
                    break;
            }

            return Math.Round(value * valueMultiplyDivideBy) / valueMultiplyDivideBy;
        }
    }
}
