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
                .Verify(p => p.PartnerContact.CreateOrUpdateAsync(It.IsAny<PartnerContactRequestModel>()), Times.Exactly(3));
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
                .Verify(p => p.PartnerContact.DeleteIfExistAsync(It.IsAny<string>()), Times.Once);

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.CreateOrUpdateAsync(It.IsAny<PartnerContactRequestModel>()), Times.Exactly(3));
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
                .Verify(p => p.PartnerContact.DeleteIfExistAsync(It.IsAny<string>()), Times.Never);

            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.CreateOrUpdateAsync(It.IsAny<PartnerContactRequestModel>()), Times.Exactly(4));
        }
    }
}
