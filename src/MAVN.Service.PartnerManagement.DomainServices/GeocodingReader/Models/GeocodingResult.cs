using System.Collections.Generic;
using Newtonsoft.Json;

namespace MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Models
{
    internal class GeocodingResult
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }
    }
}
