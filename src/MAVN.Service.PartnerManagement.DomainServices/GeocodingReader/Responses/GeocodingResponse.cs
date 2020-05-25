using System.Collections.Generic;
using MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Enums;
using MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Models;
using Newtonsoft.Json;

namespace MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Responses
{
    internal class GeocodingResponse
    {
        [JsonProperty("results")]
        public List<GeocodingResult> Result { get; set; }

        [JsonProperty("status")]
        public GeocodingResponseStatusCode Status { get; set; }
    }
}
