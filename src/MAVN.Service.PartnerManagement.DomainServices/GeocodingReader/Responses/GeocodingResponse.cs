using System.Collections.Generic;
using Newtonsoft.Json;

namespace MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Responses
{
    internal class GeocodingResponse
    {
        [JsonProperty("results")]
        public List<Result> Result { get; set; }

        [JsonProperty("status")]
        public GeocodingResponseStatusCode Status { get; set; }
    }

    internal class Result
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }
    }

    internal class AddressComponent
    {
        [JsonProperty("long_name")]
        public string CountryName { get; set; }

        [JsonProperty("short_name")]
        public string CountryIso2Code { get; set; }

        [JsonProperty("types")]
        public List<AddressTypes> Type { get; set; }
    }

    internal enum AddressTypes
    {
        Country,
        Political
    }
    internal enum GeocodingResponseStatusCode
    {
        [JsonProperty("OK")]
        Ok,
        [JsonProperty("ZERO_RESULTS")]
        ZeroResults,
        [JsonProperty("OVER_QUERY_LIMIT")]
        OverQueryLimit,
        [JsonProperty("REQUEST_DENIED")]
        RequestDenied,
        [JsonProperty("INVALID_REQUEST")]
        InvalidRequest,
        [JsonProperty("UNKNOWN_ERROR")]
        UnknownError
    }
}
