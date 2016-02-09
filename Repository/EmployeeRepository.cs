// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      EmployeeRepository.cs
// * 
// * DESCRIPTION: Employee repository. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using System.Collections.Generic;
using Models;

namespace Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        /// <summary>
        /// retrieves all employees
        /// </summary>
        /// <returns>list of employees</returns>
        public List<Employee> GetAllEmployees()
        {
            // TODO: implement this when DB is in place
            return null;
        }

        /// <summary>
        /// retrieves all dependents for an employee
        /// </summary>
        /// <param name="anEmployeeId">the id of the employee</param>
        /// <returns>list of dependents</returns>
        public List<Dependent> GetDependents(int anEmployeeId)
        {
            // TODO: implement this when DB is in place
            return null;
        }

        /// <summary>
        /// adds an employee the database
        /// </summary>
        /// <param name="anEmployee">the employee to add</param>
        /// <param name="aListOfDependents">a list of the employee's dependents</param>
        public void AddEmployee(Employee anEmployee, List<Dependent> aListOfDependents)
        {
            // TODO: implement this when DB is in place
        }

        /// <summary>
        /// adds a dependent to the database
        /// </summary>
        /// <param name="aDependent">the dependent to add</param>
        public int AddDependent(Dependent aDependent)
        {
            // TODO: implement this when DB is in place
            return -1;
        }

        /// <summary>
        /// retrieves an employee by their id
        /// </summary>
        /// <param name="anEmployeeId">the employee id of the employee</param>
        /// <returns>an employee</returns>
        public Employee GetEmployeeById(int anEmployeeId)
        {
            // TODO: implement this when DB is in place
            return null;
        }

        /// <summary>
        /// retrieves a dependent by their id
        /// </summary>
        /// <param name="anEmployeeId">the id of the dependent</param>
        /// <returns>a dependent</returns>
        public Dependent GetDependentById(int aDependentId)
        {
            // TODO: implement this when DB is in place
            return null;
        }
    }
}
