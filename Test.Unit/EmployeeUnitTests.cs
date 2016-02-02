// *******************************************************************
// * Solution:  Paylocity
// * Project:   Test.Unit
// * File:      EmployeeUnitTests.cs
// * 
// * DESCRIPTION: Unit tests for employees. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using Models;
using Repository;
using System.Collections.Generic;

namespace Test.Unit
{
    [TestClass]
    public class EmployeeUnitTests
    {
        IEmployeeRepository empRepo;
        EmployeeCalculations empCalcs;

        private const double expectedEmpDeduction = 1000.0;
        private const double expectedEmpDiscountedDeduction = 900.0;
        private const double expectedDepDeduction = 500.0;
        private const double expectedDepDiscountedDeduction = 450.0;

        // John = 1000, Abigail = 450, Aaron = 450, Sarah = 500
        // 2400 / 26 pay periods = 92.31
        private const double expectedPayrollDeduction = 92.31;

        [TestInitialize]
        public void TestInitialize()
        {
            empRepo = new TestEmployeeRepository();
            empCalcs = new EmployeeCalculations(empRepo);
        }

        [TestMethod]
        public void VerifyDeductionForEmployee()
        {
            // arrange
            Employee emp = empRepo.GetEmployeeById(1); // John

            // act
            double actualEmpDeduction = empCalcs.CalculateEmpCost(emp);

            // assert
            Assert.AreEqual(expectedEmpDeduction, actualEmpDeduction);
        }

        [TestMethod]
        public void VerifyDiscountedDeductionForEmployee()
        {
            // arrange
            Employee emp = empRepo.GetEmployeeById(2); // Angela

            // act
            double actualEmpDiscountedDeduction = empCalcs.CalculateEmpCost(emp);

            // assert
            Assert.AreEqual(expectedEmpDiscountedDeduction, actualEmpDiscountedDeduction);
        }

        [TestMethod]
        public void VerifyDeductionForDependent()
        {
            // arrange
            Dependent dep = empRepo.GetDependentById(3); // Sarah

            // act
            double actualDepDeduction = empCalcs.CalculateDependentCost(dep);

            // assert
            Assert.AreEqual(expectedDepDeduction, actualDepDeduction);
        }

        [TestMethod]
        public void VerifyDiscountedDeductionForDependent()
        {
            // arrange
            Dependent dep = empRepo.GetDependentById(1); // Abigail

            // act
            double actualDepDiscountedDeduction = empCalcs.CalculateDependentCost(dep);

            // assert
            Assert.AreEqual(expectedDepDiscountedDeduction, actualDepDiscountedDeduction);
        }

        [TestMethod]
        public void VerifyTotalPayrollDeduction()
        {
            // arrange
            Employee emp = empRepo.GetEmployeeById(1); // John

            // act
            double actualPayrollDeduction = Math.Round(empCalcs.CalculateEmpDeductions(emp), 2);

            // assert
            Assert.AreEqual(expectedPayrollDeduction, actualPayrollDeduction);
        }
    }
}
