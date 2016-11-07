using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;
using BankLoanScheme.Helpers;
using BankLoanScheme.Interfaces;

namespace BankLoanScheme.Concretes
{
    public class LowLenderService:ILowLoanLender
    {
        public decimal ComputeLenderContributaryLoan(decimal amountToLoan, Domain.LenderData lenderData)
        {
            if (lenderData.Available >= amountToLoan)
            {
                return amountToLoan;
            }
            return lenderData.Available;
        }

        public IList<Domain.LenderData> GetLenderContributors(decimal totalAmountToLoan, IList<Domain.LenderData> lendersData)
        {
            lendersData.Sort();
            decimal totalAmountOfLoanRequired = totalAmountToLoan;

            var lenderSatisfyableList = new List<LenderData>();
            decimal cumulativeAmountLent = 0;
            //now lenderData sorted from min to max;
            foreach (var lenderData in lendersData)
            {
                var amountLent = ComputeLenderContributaryLoan(totalAmountToLoan, lenderData);
                if(amountLent > 0)
                {
                    lenderData.AmountLent = amountLent;
                    totalAmountToLoan -= amountLent;
                    lenderSatisfyableList.Add(lenderData);
                }

                cumulativeAmountLent += amountLent;

                if (cumulativeAmountLent == totalAmountOfLoanRequired)
                {
                    break;
                }
            }

            return lenderSatisfyableList;
        }
    }
}
