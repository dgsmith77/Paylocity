// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      IEmployeeRepository.cs
// * 
// * DESCRIPTION: Defines the contract for employee repository implementations. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using Models;
using System.Collections.Generic;

namespace Repository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// retrieves all employees
        /// </summary>
        /// <returns>list of employees</returns>
        List<Employee> GetAllEmployees();

        /// <summary>
        /// retrieves all dependents for an employee
        /// </summary>
        /// <param name="anEmployeeId">the id of the employee</param>
        /// <returns>list of dependents</returns>
        List<Dependent> GetDependents(int anEmployeeId);

        /// <summary>
        /// adds an employee to the database
        /// </summary>
        /// <param name="anEmployee">the employee to add</param>
        /// <param name="aListOfDependents">a list of the employees dependents</param>
        void AddEmployee(Employee anEmployee, List<Dependent> aListOfDependents);

        /// <summary>
        /// adds a dependent to the database
        /// </summary>
        /// <param name="aDependent">the dependent to add</param>
        /// <returns>the dependent id</returns>
        int AddDependent(Dependent aDependent);

        /// <summary>
        /// retrieves an employee by their id
        /// </summary>
        /// <param name="anEmployeeId">the id of the employee</param>
        /// <returns>an employee</returns>
        Employee GetEmployeeById(int anEmployeeId);

        /// <summary>
        /// retrieves a dependent by their id
        /// </summary>
        /// <param name="aDependentId">the id of the dependent</param>
        /// <returns>a dependent</returns>
        Dependent GetDependentById(int aDependentId);
    }
}
