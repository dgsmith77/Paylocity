// *******************************************************************
// * Solution:  Paylocity
// * Project:   Business
// * File:      EmployeeCalculations.cs
// * 
// * DESCRIPTION: Business class to do the employee calculations
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// * 02/10/2016  dsmith     Added config repository to the constructor
// *                        Changed all doubles to decimals
// *******************************************************************
using System;
using System.Linq;
using Models;
using Repository;
using System.Collections.Generic;

namespace Business
{
    public class EmployeeCalculations
    {
        // employee repository
        private IEmployeeRepository empRepo;
        // config repository
        private IConfigItemRepository configRepo;

        // constants - keys for config repository
        private const string PAY_PERIODS = "PayPeriods";
        private const string YEARLY_COST = "YearlyCost";
        private const string DEPENDENT_COST = "DependentCost";
        private const string AVAILABLE_DISCOUNT = "AvailableDiscount";
        private const string DISCOUNT = "Discount";

        // config variables
        private int PayPeriods;
        private decimal YearlyCost;
        private decimal DependentCost;
        private string AvailableDiscount;
        private decimal Discount;
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="anEmpRepository">a class that implements the IEmployeeRepository interface</param>
        /// <param name="aConfigRepository">a class that implements the IConfigItemRepository interface</param>
        public EmployeeCalculations(IEmployeeRepository anEmpRepository, IConfigItemRepository aConfigRepository)
        {
            empRepo = anEmpRepository;
            configRepo = aConfigRepository;
            // set all config items
            PayPeriods = Int32.Parse(configRepo.GetConfigItem(PAY_PERIODS));
            YearlyCost = decimal.Parse(configRepo.GetConfigItem(YEARLY_COST));
            DependentCost = decimal.Parse(configRepo.GetConfigItem(DEPENDENT_COST));
            AvailableDiscount = configRepo.GetConfigItem(AVAILABLE_DISCOUNT);
            Discount = decimal.Parse(configRepo.GetConfigItem(DISCOUNT));
        }
 
        /// <summary>
        /// Method to calculate the per period payroll deductions
        /// </summary>
        /// <param name="anEmployee">an employee</param>
        /// <returns>pay period deduction</returns>
        public decimal CalculateEmpDeductions(Employee anEmployee)
        {
            // employee benefit cost
            decimal EmpCost = CalculateEmpCost(anEmployee);

            // dependent benefit cost
            decimal DepCost = CalculateAllDependentCost(anEmployee);

            // calculate deduction per pay period
            decimal Deduction = (EmpCost + DepCost) / PayPeriods;

            return Deduction;
        }

        /// <summary>
        /// Method to calculate the yearly benefit cost of a dependent
        /// </summary>
        /// <param name="aDependent">a dependent</param>
        /// <returns>cost of dependent</returns>
        public decimal CalculateDependentCost(Dependent aDependent)
        {
            return aDependent.Name.StartsWith(AvailableDiscount, StringComparison.CurrentCultureIgnoreCase) ?
                DependentCost - (DependentCost * Discount) :
                DependentCost;
        }

        /// <summary>
        /// Method to calculate the yearly benefit cost of an employee
        /// </summary>
        /// <param name="anEmployee">an employee</param>
        /// <returns>cost of employee</returns>
        public decimal CalculateEmpCost(Employee anEmployee)
        {
            return anEmployee.FirstName.StartsWith(AvailableDiscount, StringComparison.CurrentCultureIgnoreCase) ?
                YearlyCost - (YearlyCost * Discount) :
                YearlyCost;
        }

        /// <summary>
        /// Method to calculate pay after benefit deduction
        /// </summary>
        /// <param name="anEmployee">an employee</param>
        /// <returns>pay after benefit deduction</returns>
        public decimal CalculatePayAfterBenefitDeduction(Employee anEmployee)
        {
            return anEmployee.Salary - CalculateEmpDeductions(anEmployee);
        }

        /// <summary>
        /// Method to calculate the yearly benefit cost of all dependents
        /// </summary>
        /// <param name="anEmployee"></param>
        /// <returns></returns>
        private decimal CalculateAllDependentCost(Employee anEmployee)
        {
            // get number of dependents
            int DependentCount = anEmployee.Dependents.Count;

            if (DependentCount == 0)
            {
                return 0;
            }

            // dependents that are eligible for discount
            List<Dependent> dependents = empRepo.GetDependents(anEmployee.EmployeeId);
            int DependentDiscount = dependents.Where(d => d.Name.StartsWith(AvailableDiscount, StringComparison.CurrentCultureIgnoreCase)).Count();

            // dependent benefit cost
            return DependentCount * DependentCost - (DependentDiscount * Discount * DependentCost);
        }
    }
}
