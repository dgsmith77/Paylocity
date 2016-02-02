// *******************************************************************
// * Solution:  Paylocity
// * Project:   PaylocityWeb
// * File:      EmployeeController.cs
// * 
// * DESCRIPTION: Employee controller. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using System.Collections.Generic;
using System.Web.Mvc;
using Models;
using Repository;
using Business;
using PaylocityWeb.ViewModels;

namespace PaylocityWeb.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository repository = new MockEmployeeRepository();

        public ActionResult Index()
        {
            List<Employee> employees = repository.GetAllEmployees();
            return View(employees);
        }

        /// <summary>
        /// method to create the add employee form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult _AddEmployee()
        {
            EmpDetailsViewModel model = new EmpDetailsViewModel();
            return PartialView(model);
        }

        /// <summary>
        /// Method to add an employee
        /// </summary>
        /// <param name="employee">the employee to add</param>
        /// <returns>View</returns>
        [HttpPost]
        public void AddEmployee(EmpDetailsViewModel anEmpDetails)
        {
            IEmployeeRepository empRepo = new MockEmployeeRepository();
            EmployeeCalculations empCalcs = new EmployeeCalculations(empRepo);
            if (anEmpDetails.dependents != null)
            {
                foreach (var dependent in anEmpDetails.dependents)
                {
                    if (!string.IsNullOrWhiteSpace(dependent.Name))
                    {
                        dependent.BenefitCost = empCalcs.CalculateDependentCost(dependent);
                        int id = repository.AddDependent(dependent);
                        anEmpDetails.employee.Dependents.Add(id);
                    }
                }
            }
            anEmpDetails.employee.BenefitCost = empCalcs.CalculateEmpCost(anEmpDetails.employee);
            repository.AddEmployee(anEmpDetails.employee);
            anEmpDetails.PayrollDeduction = empCalcs.CalculateEmpDeductions(anEmpDetails.employee);
        }

        /// <summary>
        /// Method to get employees details
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>_Details View</returns>
        [HttpGet]
        public ActionResult GetDetails(int anEmployeeId)
        {
            IEmployeeRepository empRepo = new MockEmployeeRepository();
            EmployeeCalculations empCalcs = new EmployeeCalculations(empRepo);
            EmpDetailsViewModel empDetails = new EmpDetailsViewModel();
            empDetails.employee = repository.GetEmployeeById(anEmployeeId);
            empDetails.dependents = repository.GetDependents(anEmployeeId);
            empDetails.PayrollDeduction = empCalcs.CalculateEmpDeductions(empDetails.employee);
            empDetails.PayAfterBenefitDeduction = empDetails.employee.Salary - empDetails.PayrollDeduction;

            return PartialView("_Details", empDetails);
        }
    }
}
