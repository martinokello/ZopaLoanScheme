using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Concretes;
using BankLoanScheme.Domain;
using BankLoanScheme.Helpers;
using BankLoanScheme.Interfaces;

namespace BankLoanScheme
{
    class Program
    {
        static void Main(string[] args)
        {
     
            try
            {
                IZopaPrintAndIO zopaPrintAndIOService = new ZopaPrintAndIOService();
                IList<LenderData> lenderData = zopaPrintAndIOService.ReadCvsFile("market_data.txt");
                    
                /*   
                new List<LenderData>
                {
                    new LenderData {Available = 150, Lender = "Joe Blogs", Rate = (float) 0.052},
                    new LenderData {Available = 230, Lender = "Martin Okello", Rate = (float) 0.022},
                    new LenderData {Available = 650, Lender = "Joshua Lent", Rate = (float) 0.032}
                };*/

                decimal loanAmountRequest = 1010;//decimal.Parse(args[1]);
                var monthsDurationOfPayment = 36;

                if (args.Length > 0)
                {
                    lenderData = zopaPrintAndIOService.ReadCvsFile(args[0]);
                    loanAmountRequest = decimal.Parse(args[1]);
                }


                IZopaLoanPool zopaLoanService = new ZopaLoanPoolService(lenderData);

                var canZopaSatisfy = zopaLoanService.CanZopaFulFillLoan(loanAmountRequest);
                if (canZopaSatisfy)
                {
                    ILowLoanLender lowLoanLenderService = new LowLenderService();


                    var lenderLowLenderSatisiyingList = lowLoanLenderService.GetLenderContributors(loanAmountRequest, lenderData);

                    IRepayment repaymentService = new RepaymentService(lowLoanLenderService);

                    var repaymentAmount = repaymentService.ComputeTotalRepaymentAmount(lenderLowLenderSatisiyingList);
                    var interestRate = repaymentService.ComputeTotalRateOfRepayment(lenderLowLenderSatisiyingList);
                    var monthlyPayments = repaymentService.ComputeMonthlyRepaymentAmount(lenderLowLenderSatisiyingList, monthsDurationOfPayment);

                    zopaPrintAndIOService.PrintLine((decimal)repaymentService.RoundToTheNearest((double)loanAmountRequest, 2), (double)repaymentService.RoundToTheNearest(interestRate, 1), (decimal)repaymentService.RoundToTheNearest((double)monthlyPayments, 2), (decimal)repaymentService.RoundToTheNearest((double)repaymentAmount, 2));
                }
                else
                {
                    Console.Out.WriteLine(string.Format("Zopa cannot satisfy the loan request for £{0} Pounds",
                        loanAmountRequest));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Your data may be inadequate for the machine. check the Program inputs");
            }
        }
    }
}
