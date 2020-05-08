using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.PartnerManagement.Domain.Exceptions;
using MAVN.Service.PartnerManagement.Domain.Models;
using Moq;
using Xunit;

namespace MAVN.Service.PartnerManagement.Tests.DomainServices
{
    public class LocationServiceTests
    {
        [Fact]
        public async Task WhenCreateAsyncIsCalled_ThenPartnerIsCreated()
        {
            // Arrange
            var fixture = new LocationServiceTestsFixture()
                .SetupCreateAction();

            var partner = new Partner
            {
                Name = "New Partner",
                Description = "Description",
                BusinessVertical = Vertical.Hospitality,
                CreatedBy = Guid.NewGuid(),
                ClientId = "newClientId",
                ClientSecret = "newClientSecret",
                AmountInCurrency = 1,
                AmountInTokens = 2,
                Locations = new List<Location>
                {
                    new Location
                    {
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[0],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[1],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    }
                }
            };

            // Act
            await fixture.LocationService.CreateLocationsContactPersonForPartnerAsync(partner);

            // Assert
            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.CreateIfNotExistAsync(It.IsAny<PartnerContactRequestModel>()), Times.Exactly(3));
        }

        [Fact]
        public async Task WhenCreateAsyncIsCalled_ThenPartnerLoginAlreadyExists()
        {
            // Arrange
            var fixture = new LocationServiceTestsFixture()
            {
                PartnerCreateContactErrorCodes = PartnerContactErrorCodes.PartnerContactAlreadyExists
            }.SetupCreateAction();

            var partner = fixture.Partner;

            // Act
            // Assert
            await Assert.ThrowsAsync<LocationContactRegistrationFailedException>(() =>
                fixture.LocationService.CreateLocationsContactPersonForPartnerAsync(partner));
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenLocationsAreUpdatedCreatedAndDeleted()
        {

            // Arrange
            var fixture = new LocationServiceTestsFixture()
                .SetupUpdateAction();

            var partner = new Partner
            {
                Name = "New Partner",
                Description = "Description",
                BusinessVertical = Vertical.Hospitality,
                CreatedBy = Guid.NewGuid(),
                ClientId = "newClientId",
                ClientSecret = "newClientSecret",
                AmountInCurrency = 1,
                AmountInTokens = 2,
                Locations = new List<Location>
                {
                    new Location
                    {
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[0],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[1],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    }
                }
            };

            // Act
            await fixture.LocationService.UpdateRangeAsync(partner, partner.Locations, fixture.Partner.Locations);

            // Assert
            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.DeleteAsync(It.IsAny<string>()), Times.Once);

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.UpdateAsync(It.IsAny<PartnerContactUpdateRequestModel>()), Times.Exactly(2));

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.CreateIfNotExistAsync(It.IsAny<PartnerContactRequestModel>()), Times.Once);
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenLocationsAreUpdatedAndCreated()
        {

            // Arrange
            var fixture = new LocationServiceTestsFixture()
                .SetupUpdateAction();

            var partner = new Partner
            {
                Name = "New Partner",
                Description = "Description",
                BusinessVertical = Vertical.Hospitality,
                CreatedBy = Guid.NewGuid(),
                ClientId = "newClientId",
                ClientSecret = "newClientSecret",
                AmountInCurrency = 1,
                AmountInTokens = 2,
                Locations = new List<Location>
                {
                    new Location
                    {
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[0],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[1],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[2],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    }
                }
            };

            // Act
            await fixture.LocationService.UpdateRangeAsync(partner, partner.Locations, fixture.Partner.Locations);

            // Assert
            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.DeleteAsync(It.IsAny<string>()), Times.Never);

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.UpdateAsync(It.IsAny<PartnerContactUpdateRequestModel>()), Times.Exactly(3));

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.CreateIfNotExistAsync(It.IsAny<PartnerContactRequestModel>()), Times.Once);
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenLocationsAreOnlyUpdated()
        {

            // Arrange
            var fixture = new LocationServiceTestsFixture()
                .SetupUpdateAction();

            var partner = new Partner
            {
                Name = "New Partner",
                Description = "Description",
                BusinessVertical = Vertical.Hospitality,
                CreatedBy = Guid.NewGuid(),
                ClientId = "newClientId",
                ClientSecret = "newClientSecret",
                AmountInCurrency = 1,
                AmountInTokens = 2,
                Locations = new List<Location>
                {
                    new Location
                    {
                        Id = fixture.LocationIds[0],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[1],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    },
                    new Location
                    {
                        Id = fixture.LocationIds[2],
                        Name = "Hotel 1",
                        Address = "City 1",
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Name 1",
                            LastName = "Name 2",
                            Email = "Email",
                            PhoneNumber = "Phone Number"
                        }
                    }
                }
            };

            // Act
            await fixture.LocationService.UpdateRangeAsync(partner, partner.Locations, fixture.Partner.Locations);

            // Assert
            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.DeleteAsync(It.IsAny<string>()), Times.Never);

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.UpdateAsync(It.IsAny<PartnerContactUpdateRequestModel>()), Times.Exactly(3));

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.CreateIfNotExistAsync(It.IsAny<PartnerContactRequestModel>()), Times.Never);
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenPartnerContactCreateAlreadyExists()
        {
            // Arrange
            var fixture = new LocationServiceTestsFixture()
            {
                PartnerCreateContactErrorCodes = PartnerContactErrorCodes.PartnerContactAlreadyExists
            }.SetupUpdateAction();

            var partner = fixture.Partner;

            var locations = new List<Location>();
            locations.AddRange(partner.Locations);
            locations.Add(new Location
            {
                ContactPerson = new ContactPerson()
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    PhoneNumber = "88888",
                    Email = "Email@emai.com"
                }
            });

            partner.Locations = locations;

            // Act
            // Assert
            await Assert.ThrowsAsync<LocationContactRegistrationFailedException>(() =>
                fixture.LocationService.UpdateRangeAsync(partner, partner.Locations, fixture.Partner.Locations));
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenPartnerContactUpdateAlreadyExists()
        {
            // Arrange
            var fixture = new LocationServiceTestsFixture()
            {
                PartnerUpdateContactErrorCodes = PartnerContactErrorCodes.PartnerContactAlreadyExists
            }.SetupUpdateAction();

            var partner = fixture.Partner;

            // Act
            // Assert
            await Assert.ThrowsAsync<LocationContactUpdateFailedException>(() =>
                fixture.LocationService.UpdateRangeAsync(partner, partner.Locations, fixture.Partner.Locations));
        }
    }
}
