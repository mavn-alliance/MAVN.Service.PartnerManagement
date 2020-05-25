using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Services;

namespace MAVN.Service.PartnerManagement.DomainServices
{
    public class GeocodingRestService : IGeocodingRestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _url;

        public GeocodingRestService(
            IHttpClientFactory httpClientFactory,
            string apiKey,
            string url)
        {
            _httpClient = _httpClient = httpClientFactory.CreateClient();
            _apiKey = apiKey;
            _url = url;
        }

        public async Task<string> GetCountryDataByCoordinateAsync(double latitude, double longitude)
        {
            AssertCoordinatesRange(latitude, longitude);

            var requestUrl = $"{_url}?latlng={ToDotString(latitude)},{ToDotString(longitude)}&location_type=APPROXIMATE&result_type=country&key={_apiKey}";
            var response = await _httpClient.GetAsync(new Uri(requestUrl));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private static string ToDotString(double value)
            => value
                .ToString(NumberFormatInfo.InvariantInfo)
                .Trim();

        private void AssertCoordinatesRange(double latitude, double longitude)
        {
            CheckRange(latitude, 90, -90, "Latitude");
            CheckRange(longitude, 180, -180, "Longitude");
        }

        private void CheckRange(double coordinate, int max, int min, string coordinateName)
        {
            if (coordinate < min || coordinate > max)
            {
                throw new ArgumentException($"{coordinateName} value must be between {min} and {max}.");
            }
        }
    }
}
