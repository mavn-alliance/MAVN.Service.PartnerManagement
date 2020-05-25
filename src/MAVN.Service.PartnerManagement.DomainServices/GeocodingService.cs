using System;
using System.Net.Http;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PartnerManagement.DomainServices.Helpers;

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
            var requestUrl = $"{_url}?latlng={latitude.ToDotString()},{longitude.ToDotString()}&location_type=APPROXIMATE&result_type=country&key={_apiKey}";
            var response = await _httpClient.GetAsync(new Uri(requestUrl));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
