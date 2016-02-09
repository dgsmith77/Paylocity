// *******************************************************************
// * Solution:  Paylocity
// * Project:   Business
// * File:      ConfigItems.cs
// * 
// * DESCRIPTION: The configuration items. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************
using System;
using Repository;

namespace Business
{
    public class ConfigItems
    {
        IConfigItemRepository configRepo = new MockConfigItemsRepository();

        public int PayPeriods { get; set; }
        public decimal YearlyCost { get; set; }
        public decimal DependentCost { get; set; }
        public string AvailableDiscount { get; set; }
        public decimal Discount { get; set; }

        public ConfigItems()
        {
            PayPeriods = Int32.Parse(configRepo.GetConfigItem("PayPeriods"));
            YearlyCost = decimal.Parse(configRepo.GetConfigItem("YearlyCost"));
            DependentCost = decimal.Parse(configRepo.GetConfigItem("DependentCost"));
            AvailableDiscount = configRepo.GetConfigItem("AvailableDiscount");
            Discount = decimal.Parse(configRepo.GetConfigItem("Discount"));
        }
    }
}
