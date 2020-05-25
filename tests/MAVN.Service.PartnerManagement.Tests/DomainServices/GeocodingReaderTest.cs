using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Common.Log;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PartnerManagement.DomainServices.GeocodingReader;
using Moq;
using Xunit;

namespace MAVN.Service.PartnerManagement.Tests.DomainServices
{
    public class GeocodingReaderTest
    {
        private static readonly Mock<IGeocodingRestService> _geocodingRestService = new Mock<IGeocodingRestService>();
        private static readonly Mock<ILogFactory> _log = new Mock<ILogFactory>();
        private readonly IGeocodingReader _geocodingReader = new GeocodingReader(_log.Object, _geocodingRestService.Object);
        private static string CountryIso2Code = "NL";
        private static string CountryIso3Code = "NLD";

        [Fact]
        public async Task WhenGetCountryIso3CodeByCoordinateAsync_ThenReturnsIso3CountryCode()
        {
            var mockResponseData = "{\"results\" : [{" +
                               "\"access_points\" : []," +
                               "\"address_components\" : [{" +
                               "\"long_name\" : \"Netherlands\"," +
                              $" \"short_name\" : \"{CountryIso2Code}\"," +
                               " \"types\" : [ \"country\", \"political\"]" +
                               "}]}]}";
            var mockCoordinate = double.MinValue;
            _geocodingRestService.Setup(arg => arg.GetCountryDataByCoordinateAsync(It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(mockResponseData);

            var result = await _geocodingReader.GetCountryIso3CodeByCoordinateAsync(mockCoordinate, mockCoordinate);

            Assert.Equal(result, CountryIso3Code);
        }
    }
}
