// *******************************************************************
// * Solution:  Paylocity
// * Project:   Repository
// * File:      ConfigItemsRepository.cs
// * 
// * DESCRIPTION: The configuration items repository. 
// * 
// * SOFTWARE HISTORY:
// * DATE        DEVELOPER  DESCRIPTION
// * 01/30/2016  dsmith     Initial revision
// *******************************************************************

namespace Repository
{
    public class ConfigItemsRepository : IConfigItemRepository
    {
        /// <summary>
        /// retrieves the config item
        /// </summary>
        /// <param name="aKey">the key</param>
        /// <returns>the value associated with the key</returns>
        public string GetConfigItem(string aKey)
        {
            // TODO: implement this when DB is in place
            return string.Empty;
        }
    }
}
