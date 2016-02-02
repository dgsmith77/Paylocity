// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      IConfigItemsRepository.cs
// * 
// * DESCRIPTION: Defines the contract for configuration items. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************

namespace Repository
{
    public interface IConfigItemRepository
    {
        /// <summary>
        /// retrieves the config item
        /// </summary>
        /// <param name="aKey">the key</param>
        /// <returns>the value associated with the key</returns>
        string GetConfigItem(string aKey);
    }
}
