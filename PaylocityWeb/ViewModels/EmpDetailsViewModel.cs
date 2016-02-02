// *******************************************************************
// * Solution:  Paylocity
// * Project:   PaylocityWeb
// * File:      EmpDetailsViewModel.cs
// * 
// * DESCRIPTION: View model that contains all details of an employee
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using System.Collections.Generic;
using Models;

namespace PaylocityWeb.ViewModels
{
    public class EmpDetailsViewModel
    {
        public Employee employee { get; set; }

        public List<Dependent> dependents { get; set; }

        public double PayrollDeduction { get; set; }

        public double PayAfterBenefitDeduction { get; set; }
    }
}