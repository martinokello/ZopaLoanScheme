using System;
using System.Collections.Generic;
using BankLoanScheme.Concretes;
using BankLoanScheme.Domain;
using BankLoanScheme.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLoanScheme.Tests
{
    [TestClass]
    public class RepaymentServiceTest
    {
        private IList<LenderData> _lenderData = null;
        private ILowLoanLender _lowLoanLender = null;

        [TestInitialize]
        public void SetUp()
        {
            _lenderData = new List<LenderData>
            {
                new LenderData {Available = 150, Lender = "Joe Blogs", Rate = (float) 0.052},
                new LenderData {Available = 230, Lender = "Martin Okello", Rate = (float) 0.022},
                new LenderData {Available = 650, Lender = "Joshua Lent", Rate = (float) 0.032}
            };

            _lowLoanLender = new LowLenderService();
        }

        [TestMethod]
        public void Test_RoundToTheNearest()
        {
            IRepayment repaymentService = new RepaymentService(_lowLoanLender);
            double number1 = 383.6789;
            double number2 = 575.235764;
            double number3 = 4765.235764;

            number1 = repaymentService.RoundToTheNearest(number1, 1);
            number2 = repaymentService.RoundToTheNearest(number2, 3);
            number3 = repaymentService.RoundToTheNearest(number3, 2);
            Assert.AreEqual(number1, 383.7);
            Assert.AreEqual(number2, 575.236);
            Assert.AreEqual(number3, 4765.24);
        }
        [TestMethod]
        public void Test_ComputeAmountRepayable_Given_Lent_100()
        {
            _lenderData[0].AmountLent = 100;
            _lenderData[1].AmountLent = 100;
            _lenderData[2].AmountLent = 100;

            IRepayment repaymentJobeBlogs = new RepaymentService(_lowLoanLender);
            IRepayment repaymentMartinOkello = new RepaymentService(_lowLoanLender);
            IRepayment repaymentJoshuaLent = new RepaymentService(_lowLoanLender);

            var repayableJB = (decimal)repaymentJobeBlogs.RoundToTheNearest((double)repaymentJobeBlogs.ComputeAmountRepayable(_lenderData[0]), 2);
            var repayableMO = (decimal)repaymentMartinOkello.RoundToTheNearest((double)repaymentMartinOkello.ComputeAmountRepayable(_lenderData[1]), 2);
            var repayableJL = (decimal)repaymentJoshuaLent.RoundToTheNearest((double)repaymentJoshuaLent.ComputeAmountRepayable(_lenderData[2]), 2);

            Assert.AreEqual(repayableJB,(decimal) 105.2);
            Assert.AreEqual(repayableMO, (decimal)102.2);
            Assert.AreEqual(repayableJL, (decimal)103.2);
        }
        [TestMethod]
        public void Test_ComputeMonthlyRepaymentAmount_Given_Lent_100_repayable_18_months()
        {
            _lenderData[0].AmountLent = 100;
            _lenderData[1].AmountLent = 100;
            _lenderData[2].AmountLent = 100;

            IRepayment repaymentService = new RepaymentService(_lowLoanLender);

            var monthlyRepayments = (decimal)repaymentService.RoundToTheNearest((double)repaymentService.ComputeMonthlyRepaymentAmount(_lenderData, 18),2);
            Assert.AreEqual(monthlyRepayments, (decimal)17.26);
        }
        [TestMethod]
        public void Test_ComputeTotalRepaymentAmount_Given_Lent_100()
        {
            _lenderData[0].AmountLent = 100;
            _lenderData[1].AmountLent = 100;
            _lenderData[2].AmountLent = 100;

            IRepayment repaymentService = new RepaymentService(_lowLoanLender);

            var repayable = (decimal)repaymentService.RoundToTheNearest((double)repaymentService.ComputeTotalRepaymentAmount(_lenderData), 2);

            Assert.AreEqual(repayable, (decimal)310.6);
        }

        [TestMethod]
        public void Test_ComputeTotalRateOfRepayment_Given_Lent_100()
        {
            _lenderData[0].AmountLent = 100;
            _lenderData[1].AmountLent = 100;
            _lenderData[2].AmountLent = 100;

            IRepayment repaymentService = new RepaymentService(_lowLoanLender);

            var rate = repaymentService.ComputeTotalRateOfRepayment(_lenderData);

            Assert.AreEqual(rate, 3.53);
        }
    }
}
