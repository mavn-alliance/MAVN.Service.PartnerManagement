using System.Collections.Generic;
using MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Enums;
using Newtonsoft.Json;

namespace MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Models
{
    internal class AddressComponent
    {
        [JsonProperty("long_name")]
        public string CountryName { get; set; }

        [JsonProperty("short_name")]
        public string CountryIso2Code { get; set; }

        [JsonProperty("types")]
        public List<AddressTypes> Type { get; set; }
    }

}
