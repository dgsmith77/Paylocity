// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      MockEmployeeRepository.cs
// * 
// * DESCRIPTION: Simulates an actual employee repository. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// * 02/10/2016  dsmith     tweaked some of the linq statements
// *                        Changed all doubles to decimals
// *******************************************************************
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Repository
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        // mock data
        public static class MockData
        {
            public static List<Employee> employees = new List<Employee>();
            public static List<Dependent> dependents = new List<Dependent>();
        }

        private IConfigItemRepository configRepo = new MockConfigItemsRepository();

        /// <summary>
        /// retrieves all employees
        /// </summary>
        /// <returns>list of employees</returns>
        public List<Employee> GetAllEmployees()
        {
            return MockData.employees ?? new List<Employee>();
        }

        public List<Dependent> GetDependents(int anEmployeeId)
        {
            Employee employee = GetEmployeeById(anEmployeeId);
            List<Dependent> empDependents = new List<Dependent>();
            foreach (int id in employee.Dependents)
            {
                empDependents.Add(MockData.dependents.Where(d => d.DependentId == id).FirstOrDefault());
            }
            return empDependents;
        }

        /// <summary>
        /// adds an employee to the list
        /// </summary>
        /// <param name="anEmployee">the employee to insert</param>
        /// <param name="aListOfDependents">a list of the employee's dependents</param>
        public void AddEmployee(Employee anEmployee, List<Dependent> aListOfDependents)
        {
            if (aListOfDependents != null)
            {
                foreach (var dependent in aListOfDependents)
                {
                    if (!string.IsNullOrWhiteSpace(dependent.Name))
                    {
                        int id = AddDependent(dependent);
                        anEmployee.Dependents.Add(id);
                    }
                }
            }
            anEmployee.EmployeeId = MockData.employees.Count + 1;
            anEmployee.Salary = decimal.Parse(configRepo.GetConfigItem("Salary"));
            MockData.employees.Add(anEmployee);
        }

        /// <summary>
        /// adds a dependent to the list
        /// </summary>
        /// <param name="aDependent">the dependent to add</param>
        public int AddDependent(Dependent aDependent)
        {
            aDependent.DependentId = MockData.dependents.Count + 1;
            MockData.dependents.Add(aDependent);
            return aDependent.DependentId;
        }

        /// <summary>
        /// retrieves an employee by their id
        /// </summary>
        /// <param name="anEmployeeId">the employee id of the employee</param>
        /// <returns>an employee</returns>
        public Employee GetEmployeeById(int anEmployeeId)
        {
            Employee employee = MockData.employees.Where(x => x.EmployeeId == anEmployeeId).FirstOrDefault();
            return employee ?? new Employee();
        }

        /// <summary>
        /// retrieves a dependent by their id
        /// </summary>
        /// <param name="aDependentId">the id of the dependent</param>
        /// <returns>a dependent</returns>
        public Dependent GetDependentById(int aDependentId)
        {
            Dependent dependent = MockData.dependents.Where(x => x.DependentId == aDependentId).FirstOrDefault();
            return dependent;
        }
    }
}
