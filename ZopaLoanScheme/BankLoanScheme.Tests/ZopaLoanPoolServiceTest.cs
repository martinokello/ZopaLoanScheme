using System;
using System.Collections;
using System.Collections.Generic;
using BankLoanScheme.Concretes;
using BankLoanScheme.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLoanScheme.Tests
{
    [TestClass]
    public class ZopaLoanPoolServiceTest
    {
        private IList<LenderData> _lenderData = null;

        [TestInitialize]
        public void SetUp()
        {
            _lenderData = new List<LenderData>
            {
                new LenderData {Available = 150, Lender = "Joe Blogs", Rate = (float) 0.052},
                new LenderData {Available = 230, Lender = "Martin Okello", Rate = (float) 0.022},
                new LenderData {Available = 650, Lender = "Joshua Lent", Rate = (float) 0.032}
            };

        } 

        [TestMethod]
        public void Test_ComputeZopaLoanPool()
        {
            var zopaLoanPoolService = new ZopaLoanPoolService(_lenderData);

            var totalLoanAvailable = zopaLoanPoolService.ComputeZopaLoanPool();

            Assert.AreEqual(totalLoanAvailable, 1030);

        }

        [TestMethod]
        public void Test_CanZopaFulFillLoan_Given_Loan_Is_1500_Can_Fullfill()
        {
            var zopaLoanPoolService = new ZopaLoanPoolService(_lenderData);
            var loanRequest = 1000;

            var isSatisfiable = zopaLoanPoolService.CanZopaFulFillLoan(loanRequest);

            Assert.IsTrue(isSatisfiable);

        }

        [TestMethod]
        public void Test_CanZopaFulFillLoan_Given_Loan_Is_1030_Can_Fullfill()
        {
            var zopaLoanPoolService = new ZopaLoanPoolService(_lenderData);
            var loanRequest = 1030;

            var isSatisfiable = zopaLoanPoolService.CanZopaFulFillLoan(loanRequest);

            Assert.IsTrue(isSatisfiable);

        }

        [TestMethod]
        public void Test_CanZopaFulFillLoan_Given_Loan_Is_1400_Can_not_Fullfill()
        {
            var zopaLoanPoolService = new ZopaLoanPoolService(_lenderData);
            var loanRequest = 1400;

            var isSatisfiable = zopaLoanPoolService.CanZopaFulFillLoan(loanRequest);

            Assert.IsFalse(isSatisfiable);

        }

        [TestMethod]
        public void Test_CanZopaFulFillLoan_Given_Loan_Is_15001_Or_999_Can_not_Fullfill()
        {
            var zopaLoanPoolService = new ZopaLoanPoolService(_lenderData);
            var loanRequest1 = 15001;
            var loanRequest2 = 999;

            var isSatisfiableloan1 = zopaLoanPoolService.CanZopaFulFillLoan(loanRequest1);
            var isSatisfiableloan2 = zopaLoanPoolService.CanZopaFulFillLoan(loanRequest1);

            Assert.IsFalse(isSatisfiableloan1 && isSatisfiableloan2);

        }
    }
}
