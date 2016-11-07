using System;
using System.Collections.Generic;
using BankLoanScheme.Concretes;
using BankLoanScheme.Domain;
using BankLoanScheme.Helpers;
using BankLoanScheme.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLoanScheme.Tests
{
    [TestClass]
    public class LowLoanLenderServiceTest
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
        public void Test_ComputeLenderContributaryLoan_For_Loan_Satisfiable_Loan_Amount_700()
        {
            var _loanAmount = 700;
            var lenderDataJoeBlogs = _lenderData[0];
            var lenderDataMartinOkello = _lenderData[1];
            var lenderDataJoshuaLent = _lenderData[2];

            var contributionMartinOkello = _lowLoanLender.ComputeLenderContributaryLoan(_loanAmount, lenderDataMartinOkello);
            var contributionJoshuaLent = _lowLoanLender.ComputeLenderContributaryLoan(_loanAmount - contributionMartinOkello, lenderDataJoshuaLent);
            var contributionJoeBlogs = _lowLoanLender.ComputeLenderContributaryLoan(_loanAmount - (contributionMartinOkello + contributionJoshuaLent), lenderDataJoeBlogs);

            Assert.AreEqual(230, contributionMartinOkello);
            Assert.AreEqual(470, contributionJoshuaLent);
            Assert.AreEqual(0, contributionJoeBlogs);
        }
        [TestMethod]
        public void Test_Helper_Sort_Extension_Method_According_To_Loan_Rate()
        {
            _lenderData.Sort();

            Assert.IsTrue(_lenderData[0].Lender.Equals("Martin Okello"));
            Assert.IsTrue(_lenderData[1].Lender.Equals("Joshua Lent"));
            Assert.IsTrue(_lenderData[2].Lender.Equals("Joe Blogs"));
        }
        [TestMethod]
        public void Test_GetLenderContributors_For_Loan_Satisfiable_Loan_Amount_700()
        {
            var _loanAmount = 700;

            var resultOfListLoanners = _lowLoanLender.GetLenderContributors(700, _lenderData);

            Assert.AreEqual(2, resultOfListLoanners.Count);
            Assert.IsTrue(resultOfListLoanners[0].Lender.Equals("Martin Okello"));
            Assert.IsTrue(_lenderData[1].Lender.Equals("Joshua Lent"));
        }

        [TestMethod]
        public void Test_GetLenderContributors_For_Loan_Satisfiable_Ratio_By_Contributor_Loan_Amount_700()
        {
            var _loanAmount = 700;

            var resultOfListLoanners = _lowLoanLender.GetLenderContributors(700, _lenderData);

            Assert.AreEqual(2, resultOfListLoanners.Count);
            Assert.IsTrue(resultOfListLoanners[0].AmountLent == 230);
            Assert.IsTrue(resultOfListLoanners[1].AmountLent == 470);
        }
    }
}
