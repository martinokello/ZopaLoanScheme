using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoanScheme.Domain;

namespace BankLoanScheme.Helpers
{
    public static class Helpers
    {
        public static void Sort(this IList<LenderData> lenderData)
        {
            for (var i = 0; i < lenderData.Count; i++) 
            {
                for (var j = 0; j < lenderData.Count; j++)
                {
                    if (lenderData[j].Rate > lenderData[i].Rate)
                    {
                        var tmpData = lenderData[j];

                        lenderData[j] = lenderData[i];
                        lenderData[i] = tmpData;
                    }
                }
            }
        }
    }
}
