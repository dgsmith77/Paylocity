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
// * 02/10/2016  dsmith     Moved all business logic to the Business project
// *                        Added constructors
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
        private IEmployeeRepository empRepo;
        private IConfigItemRepository configRepo;

        public EmployeeController()
        {
            empRepo = new MockEmployeeRepository();
            configRepo = new MockConfigItemsRepository();
        }
        public EmployeeController(IEmployeeRepository anEmpRepository, IConfigItemRepository aConfigRepository)
        {
            empRepo = anEmpRepository;
            configRepo = aConfigRepository;
        }

        public ActionResult Index()
        {
            List<Employee> employees = empRepo.GetAllEmployees();
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
            EmployeeCalculations empCalcs = new EmployeeCalculations(empRepo, configRepo);
            empRepo.AddEmployee(anEmpDetails.employee, anEmpDetails.dependents);
        }

        /// <summary>
        /// Method to get employees details
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>_Details View</returns>
        [HttpGet]
        public ActionResult GetDetails(int anEmployeeId)
        {
            EmployeeCalculations empCalcs = new EmployeeCalculations(empRepo, configRepo);
            EmpDetailsViewModel empDetails = new EmpDetailsViewModel();
            empDetails.employee = empRepo.GetEmployeeById(anEmployeeId);
            empDetails.employee.BenefitCost = empCalcs.CalculateEmpCost(empDetails.employee);
            empDetails.dependents = empRepo.GetDependents(anEmployeeId);
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
