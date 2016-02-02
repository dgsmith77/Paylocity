// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      MockConfigItemsRepository.cs
// * 
// * DESCRIPTION: Simulation of the configuration items repository. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class MockConfigItemsRepository : IConfigItemRepository
    {
        // represents key-value pair config items (probably stored in a database)
        Dictionary<string, string> ConfigItems = new Dictionary<string, string>()
        {
            {"Salary", "2000.00"},
            {"PayPeriods", "26"},
            {"YearlyCost", "1000.00"},
            {"DependentCost", "500.00"},
            {"AvailableDiscount", "A"},
            {"Discount", "0.1"}
        };

        /// <summary>
        /// retrieves the config item
        /// </summary>
        /// <param name="aKey">the key</param>
        /// <returns>the value associated with the key</returns>
        public string GetConfigItem(string aKey)
        {
            return ConfigItems.FirstOrDefault(c => c.Key.Equals(aKey)).Value;
        }
    }
}
