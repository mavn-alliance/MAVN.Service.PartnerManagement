using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Enums;
using MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Responses;
using Newtonsoft.Json;
using AddressTypes = MAVN.Service.PartnerManagement.DomainServices.GeocodingReader.Enums.AddressTypes;

namespace MAVN.Service.PartnerManagement.DomainServices.GeocodingReader
{
    public class GeocodingReader : IGeocodingReader
    {
        private readonly ILog _log;
        private readonly IGeocodingRestService _geocodingRestService;

        public GeocodingReader(
            ILogFactory logFactory,
            IGeocodingRestService geocodingRestService)
        {
            _geocodingRestService = geocodingRestService;
            _log = logFactory.CreateLog(this);
        }

        public async Task<string> GetCountryIso3CodeByCoordinateAsync(double latitude, double longitude)
        {
            var countryIso2Code = await GetCountryIso2CodeByCoordinateAsync(latitude, longitude);

            return !string.IsNullOrEmpty(countryIso2Code)
                ? CountryManager.Iso2ToIso3(countryIso2Code)
                : null;
        }

        private async Task<string> GetCountryIso2CodeByCoordinateAsync(double latitude, double longitude)
        {
            var geocodingData = await _geocodingRestService.GetCountryDataByCoordinateAsync(latitude, longitude);
            var geocodingResponse = JsonConvert.DeserializeObject<GeocodingResponse>(geocodingData);

            if (geocodingResponse.Status == GeocodingResponseStatusCode.Ok)
            {
                return FindCountryIso2Code(geocodingResponse);
            }

            LogFailedGeocodingResponse(geocodingResponse.Status);
            return null;
        }

        private string FindCountryIso2Code(GeocodingResponse response)
            => response?.Result?
                .FirstOrDefault()?.AddressComponents
                .FirstOrDefault(x => x.Type.Contains(AddressTypes.Country) && x.Type.Contains(AddressTypes.Political))?
                .CountryIso2Code;

        private void LogFailedGeocodingResponse(GeocodingResponseStatusCode responseStatusCode)
        {
            switch (responseStatusCode)
            {
                case GeocodingResponseStatusCode.ZeroResults:
                    _log.Warning("The reverse geocoding was successful but returned no results. This may occur if the geocoder was passed a latlng in a remote location.");
                    break;
                case GeocodingResponseStatusCode.OverQueryLimit:
                    _log.Warning("The reverse geocoding was over quota");
                    break;
                case GeocodingResponseStatusCode.RequestDenied:
                    _log.Warning("The request was denied. Possibly because the request includes a result_type or location_type parameter but does not include an API key or client ID.");
                    break;
                case GeocodingResponseStatusCode.InvalidRequest:
                    _log.Warning("The query (address, components or latlng) is missing or an invalid result_type or location_type was given.");
                    break;
                case GeocodingResponseStatusCode.UnknownError:
                    _log.Warning("The request could not be processed due to a server error.");
                    break;
            }
        }
    }
}
