using Newtonsoft.Json;

namespace MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Enums
{
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
