using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;

namespace BankLoanScheme.Interfaces
{
    public interface IZopaPrintAndIO
    {
        void PrintLine(decimal loanRequestAmount, double rate, decimal monthlyRepayment, decimal totalRepayment);
        IList<Domain.LenderData> ReadCvsFile(string cvsFilePath);
    }
}
