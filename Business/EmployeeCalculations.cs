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
        IEmployeeRepository repository;
        
        public EmployeeCalculations(IEmployeeRepository anEmpRepository)
        {
            repository = anEmpRepository;
        }

        // config items
        ConfigItems configItems = new ConfigItems();
 
        /// <summary>
        /// Method to calculate the per period payroll deductions
        /// </summary>
        /// <param name="anEmployee">an employee</param>
        /// <returns>pay period deduction</returns>
        public double CalculateEmpDeductions(Employee anEmployee)
        {
            // employee benefit cost
            double EmpCost = CalculateEmpCost(anEmployee);

            // dependent benefit cost
            double DepCost = CalculateAllDependentCost(anEmployee);

            // calculate deduction per pay period
            double Deduction = (EmpCost + DepCost) / configItems.PayPeriods;

            return Deduction;
        }

        /// <summary>
        /// Method to calculate the yearly benefit cost of a dependent
        /// </summary>
        /// <param name="aDependent">a dependent</param>
        /// <returns>cost of dependent</returns>
        public double CalculateDependentCost(Dependent aDependent)
        {
            return aDependent.Name.StartsWith(configItems.AvailableDiscount, StringComparison.CurrentCultureIgnoreCase) ?
                configItems.DependentCost - (configItems.DependentCost * configItems.Discount) :
                configItems.DependentCost;
        }

        /// <summary>
        /// Method to calculate the yearly benefit cost of an employee
        /// </summary>
        /// <param name="anEmployee">an employee</param>
        /// <returns>cost of employee</returns>
        public double CalculateEmpCost(Employee anEmployee)
        {
            return anEmployee.FirstName.StartsWith(configItems.AvailableDiscount, StringComparison.CurrentCultureIgnoreCase) ?
                configItems.YearlyCost - (configItems.YearlyCost * configItems.Discount) :
                configItems.YearlyCost;
        }


        /// <summary>
        /// Method to calculate the yearly benefit cost of all dependents
        /// </summary>
        /// <param name="anEmployee"></param>
        /// <returns></returns>
        private double CalculateAllDependentCost(Employee anEmployee)
        {
            // get number of dependents
            int DependentCount = anEmployee.Dependents.Count;

            if (DependentCount == 0)
            {
                return 0.0;
            }

            // dependents that are eligible for discount
            List<Dependent> dependents = repository.GetDependents(anEmployee.EmployeeId);
            int DependentDiscount = dependents.Where(d => d.Name.StartsWith(configItems.AvailableDiscount, StringComparison.CurrentCultureIgnoreCase)).Count();

            // dependent benefit cost
            return DependentCount * configItems.DependentCost - (DependentDiscount * configItems.Discount * configItems.DependentCost);
        }
    }
}
