// *******************************************************************
// * Solution:  Paylocity
// * Project:   Models
// * File:      Dependent.cs
// * 
// * DESCRIPTION: Dependent model. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// * 02/10/2016  dsmith     Changed BenefitCost to a decimal
// *******************************************************************

namespace Models
{
    /// <summary>
    /// represents a dependent
    /// </summary>
    public class Dependent
    {
        public int DependentId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal BenefitCost { get; set; }
    }
}
