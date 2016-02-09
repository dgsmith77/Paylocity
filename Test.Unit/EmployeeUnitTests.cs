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

        private const decimal expectedEmpDeduction = 1000;
        private const decimal expectedEmpDiscountedDeduction = 900;
        private const decimal expectedDepDeduction = 500;
        private const decimal expectedDepDiscountedDeduction = 450;

        // John = 1000, Abigail = 450, Aaron = 450, Sarah = 500
        // 2400 / 26 pay periods = 92.31
        private const decimal expectedPayrollDeduction = 92.31m;

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
            decimal actualEmpDeduction = empCalcs.CalculateEmpCost(emp);

            // assert
            Assert.AreEqual(expectedEmpDeduction, actualEmpDeduction);
        }

        [TestMethod]
        public void VerifyDiscountedDeductionForEmployee()
        {
            // arrange
            Employee emp = empRepo.GetEmployeeById(2); // Angela

            // act
            decimal actualEmpDiscountedDeduction = empCalcs.CalculateEmpCost(emp);

            // assert
            Assert.AreEqual(expectedEmpDiscountedDeduction, actualEmpDiscountedDeduction);
        }

        [TestMethod]
        public void VerifyDeductionForDependent()
        {
            // arrange
            Dependent dep = empRepo.GetDependentById(3); // Sarah

            // act
            decimal actualDepDeduction = empCalcs.CalculateDependentCost(dep);

            // assert
            Assert.AreEqual(expectedDepDeduction, actualDepDeduction);
        }

        [TestMethod]
        public void VerifyDiscountedDeductionForDependent()
        {
            // arrange
            Dependent dep = empRepo.GetDependentById(1); // Abigail

            // act
            decimal actualDepDiscountedDeduction = empCalcs.CalculateDependentCost(dep);

            // assert
            Assert.AreEqual(expectedDepDiscountedDeduction, actualDepDiscountedDeduction);
        }

        [TestMethod]
        public void VerifyTotalPayrollDeduction()
        {
            // arrange
            Employee emp = empRepo.GetEmployeeById(1); // John

            // act
            decimal actualPayrollDeduction = Math.Round(empCalcs.CalculateEmpDeductions(emp), 2);

            // assert
            Assert.AreEqual(expectedPayrollDeduction, actualPayrollDeduction);
        }
    }
}
