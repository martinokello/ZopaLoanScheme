using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Interfaces;

namespace BankLoanScheme.Concretes
{
    public class ZopaLoanPoolService:IZopaLoanPool
    {
        private IList<Domain.LenderData> _lendersList;

        public ZopaLoanPoolService(IList<Domain.LenderData> lenderList)
        {
            _lendersList = lenderList;
        }

        public decimal ComputeZopaLoanPool()
        {
            decimal amount = 0;

            foreach (var data in _lendersList)
            {
                amount += data.Available;
            }

            return amount;
        }

        public bool CanZopaFulFillLoan(decimal loanRequest)
        {
            if(loanRequest<1000 || loanRequest > 15000)
            return false;
            return loanRequest <= ComputeZopaLoanPool();
        }
    }
}
