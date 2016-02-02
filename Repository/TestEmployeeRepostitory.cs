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
// *******************************************************************
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Repository
{
    public class TestEmployeeRepository : IEmployeeRepository
    {
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

        IConfigItemRepository configRepo = new MockConfigItemsRepository();

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
        public void AddEmployee(Employee anEmployee)
        {
            anEmployee.EmployeeId = employees.Count + 1;
            anEmployee.Salary = double.Parse(configRepo.GetConfigItem("Salary"));
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
            Employee employee = employees.Find(x => x.EmployeeId.Equals(anEmployeeId));
            return employee ?? new Employee();
        }

        /// <summary>
        /// retrieves a dependent by their id
        /// </summary>
        /// <param name="anEmployeeId">the id of the dependent</param>
        /// <returns>a dependent</returns>
        public Dependent GetDependentById(int aDependentId)
        {
            Dependent dependent = dependents.Find(x => x.DependentId.Equals(aDependentId));
            return dependent;
        }
    }
}
