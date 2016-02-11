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
// * 02/10/2016  dsmith     Changed all doubles to decimals
// *******************************************************************
using System.Collections.Generic;
using Models;

namespace PaylocityWeb.ViewModels
{
    public class EmpDetailsViewModel
    {
        public Employee employee { get; set; }

        public List<Dependent> dependents { get; set; }

        public decimal PayrollDeduction { get; set; }

        public decimal PayAfterBenefitDeduction { get; set; }
    }
}