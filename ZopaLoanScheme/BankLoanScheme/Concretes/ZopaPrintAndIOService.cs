using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;
using BankLoanScheme.Interfaces;

namespace BankLoanScheme.Concretes
{
    public class ZopaPrintAndIOService:IZopaPrintAndIO
    {
        public void PrintLine(decimal loanRequestAmount,double rate, decimal monthlyRepayment, decimal totalRepayment)
        {
            var printText = string.Format(
@"Requested amount: £{0}
Rate: {1}%
Monthly repayment: £{2}
Total repayment: £{3}",loanRequestAmount,rate,monthlyRepayment,totalRepayment);

            Console.Out.WriteLine(printText);
            Console.Out.WriteLine(System.Environment.NewLine);
        }


        public IList<Domain.LenderData> ReadCvsFile(string cvsFilePath)
        {
            var fileInfo = new FileInfo(cvsFilePath);
            var lenderData = new List<LenderData>();

            if (!fileInfo.Exists)
            {
                Console.Out.WriteLine("Please ensure the csv input Loans/Lender details exists inside the Program's directory!!");
                throw(new Exception("Please ensure the csv input Loans/Lender details exists inside the Program's directory!!"));
            }

            var stream = fileInfo.OpenText();
            var cvsContent = stream.ReadToEnd();

            var records = cvsContent.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var enumerator = records.GetEnumerator();

            enumerator.MoveNext();

            while (enumerator.MoveNext())
            {
                var record = enumerator.Current.ToString();
                var cells = record.Split(new[] {',', ' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

                lenderData.Add(new LenderData
                {
                    Lender = cells[0],
                    Rate = double.Parse(cells[1]),
                    Available = decimal.Parse(cells[2]),
                    AmountLent = decimal.Parse("0.00")
                });
            }

            return lenderData;
        }
    }
}
