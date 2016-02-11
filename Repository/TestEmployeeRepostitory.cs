// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      TestEmployeeRepository.cs
// * 
// * DESCRIPTION: an employee repository for testing. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// * 02/10/2016  dsmith     Changed all doubles to decimals
// *******************************************************************
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Repository
{
    public class TestEmployeeRepository : IEmployeeRepository
    {
        // config repository
        private IConfigItemRepository configRepo = new MockConfigItemsRepository();

        // test data
        public List<Employee> employees = new List<Employee>
        {
            new Employee 
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Smith",
                Dependents = new List<int>{1, 2, 3}
            },
             new Employee 
            {
                EmployeeId = 2,
                FirstName = "Angela",
                LastName = "Foster"
            }
        };
        public List<Dependent> dependents = new List<Dependent>
        {
            new Dependent
            {
                DependentId = 1,
                Name="Abigail",
                Type="Spouse"
            },
            new Dependent
            {
                DependentId = 2,
                Name="Aaron",
                Type="Child"
            },
            new Dependent
            {
                DependentId = 3,
                Name="Sarah",
                Type="Child"
            }
        };

        /// <summary>
        /// retrieves all employees
        /// </summary>
        /// <returns>list of employees</returns>
        public List<Employee> GetAllEmployees()
        {
            return employees ?? new List<Employee>();
        }

        public List<Dependent> GetDependents(int anEmployeeId)
        {
            Employee employee = GetEmployeeById(anEmployeeId);
            List<Dependent> empDependents = new List<Dependent>();
            foreach (int id in employee.Dependents)
            {
                empDependents.Add(dependents.Where(d => d.DependentId.Equals(id)).FirstOrDefault());
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
            anEmployee.EmployeeId = employees.Count + 1;
            anEmployee.Salary = decimal.Parse(configRepo.GetConfigItem("Salary"));
            employees.Add(anEmployee);
        }

        /// <summary>
        /// adds a dependent to the list
        /// </summary>
        /// <param name="aDependent">the dependent to add</param>
        public int AddDependent(Dependent aDependent)
        {
            aDependent.DependentId = dependents.Count + 1;
            dependents.Add(aDependent);
            return aDependent.DependentId;
        }

        /// <summary>
        /// retrieves an employee by their id
        /// </summary>
        /// <param name="anEmployeeId">the employee id of the employee</param>
        /// <returns>an employee</returns>
        public Employee GetEmployeeById(int anEmployeeId)
        {
            Employee employee = employees.Where(x => x.EmployeeId == anEmployeeId).FirstOrDefault();                          
            return employee ?? new Employee();
        }

        /// <summary>
        /// retrieves a dependent by their id
        /// </summary>
        /// <param name="aDependentId">the id of the dependent</param>
        /// <returns>a dependent</returns>
        public Dependent GetDependentById(int aDependentId)
        {
            Dependent dependent = dependents.Where(x => x.DependentId == aDependentId).FirstOrDefault();
            return dependent;
        }
    }
}
