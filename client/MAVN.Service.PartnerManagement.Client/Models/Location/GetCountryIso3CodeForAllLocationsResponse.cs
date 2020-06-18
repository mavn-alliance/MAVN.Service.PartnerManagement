using System.Collections.Generic;

namespace MAVN.Service.PartnerManagement.Client.Models.Location
{
    /// <summary>
    /// Response model
    /// </summary>
    public class GetCountryIso3CodeForAllLocationsResponse
    {
        /// <summary>
        /// List of iso3 codes
        /// </summary>
        public IReadOnlyList<string> CountriesIso3Codes { get; set; }
    }
}
