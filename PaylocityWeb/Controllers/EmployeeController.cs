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
        IEmployeeRepository repository;

        public EmployeeController()
        {
            repository = new MockEmployeeRepository();
        }
        public EmployeeController(IEmployeeRepository anEmpRepository)
        {
            repository = anEmpRepository;
        }

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
            EmployeeCalculations empCalcs = new EmployeeCalculations(repository);
            repository.AddEmployee(anEmpDetails.employee, anEmpDetails.dependents);
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
            empDetails.employee.BenefitCost = empCalcs.CalculateEmpCost(empDetails.employee);
            empDetails.dependents = repository.GetDependents(anEmployeeId);
            foreach (var dependent in empDetails.dependents)
            {
                dependent.BenefitCost = empCalcs.CalculateDependentCost(dependent);
            }
            empDetails.PayrollDeduction = empCalcs.CalculateEmpDeductions(empDetails.employee);
            empDetails.PayAfterBenefitDeduction = empCalcs.CalculatePayAfterBenefitDeduction(empDetails.employee);

            return PartialView("_Details", empDetails);
        }
    }
}
