using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;

namespace BankLoanScheme.Interfaces
{
    public interface IZopaLoanPool
    {
        decimal ComputeZopaLoanPool();
        bool CanZopaFulFillLoan(decimal loanRequest);
    }
}
