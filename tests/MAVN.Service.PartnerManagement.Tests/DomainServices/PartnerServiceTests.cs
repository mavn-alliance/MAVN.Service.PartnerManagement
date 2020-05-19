using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.Credentials.Client;
using MAVN.Service.Credentials.Client.Models.Requests;
using MAVN.Service.Credentials.Client.Models.Responses;
using MAVN.Service.PartnerManagement.Contract;
using MAVN.Service.PartnerManagement.Domain.Exceptions;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Models.Dto;
using Moq;
using Xunit;

namespace MAVN.Service.PartnerManagement.Tests.DomainServices
{
    public class PartnerServiceTests
    {
        [Fact]
        public async Task WhenGetAsyncIsCalled_ThenPaginatedListIsReturnedWithData()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
                .SetupGetActions();

            // Act
            var partners = await fixture.PartnerService.GetAsync(new PartnerListRequestDto { CurrentPage = 1, PageSize = 10 });

            // Assert
            Assert.Equal(fixture.Partners, partners);
        }

        [Fact]
        public async Task WhenGetAsyncIsCalled_ThenPaginatedListIsReturnedWithoutData()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partners = (new List<Partner>(), 0)
            }.SetupGetActions();

            // Act
            var partners = await fixture.PartnerService.GetAsync(new PartnerListRequestDto { CurrentPage = 1, PageSize = 10 });

            // Assert
            Assert.Equal(fixture.Partners, partners);
        }

        [Fact]
        public async Task WhenGetByIdAsyncIsCalled_ThenPartnerIsReturned()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture().SetupGetActions();

            // Act
            var partner = await fixture.PartnerService.GetByIdAsync(fixture.Partner.Id);

            // Assert
            Assert.Equal(fixture.Partner, partner);
        }

        [Fact]
        public async Task WhenGetByIdAsyncIsCalled_ThenPartnerIsNotReturned()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partner = null
            }.SetupGetActions();

            // Act
            var partner = await fixture.PartnerService.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(partner);
        }

        [Fact]
        public async Task WhenGetByClientIdAsyncIsCalled_ThenPartnerIsReturned()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture().SetupGetActions();

            // Act
            var partner = await fixture.PartnerService.GetByClientIdAsync(fixture.Partner.ClientId);

            // Assert
            Assert.Equal(fixture.Partner, partner);
        }

        [Fact]
        public async Task WhenGetByClientIdAsyncIsCalled_ThenPartnerIsNotReturned()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partner = null
            }.SetupGetActions();

            // Act
            var partner = await fixture.PartnerService.GetByClientIdAsync("NonExistentClient");

            // Assert
            Assert.Null(partner);
        }

        [Fact]
        public async Task WhenGetByLocationIdAsyncIsCalled_ThenPartnerIsReturned()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture().SetupGetActions();

            // Act
            var partner = await fixture.PartnerService.GetByLocationIdAsync(fixture.Partner.Id);

            // Assert
            Assert.Equal(fixture.Partner, partner);
        }

        [Fact]
        public async Task WhenGetByLocationIdAsyncIsCalled_ThenPartnerIsNotReturned()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partner = null
            }.SetupGetActions();

            // Act
            var partner = await fixture.PartnerService.GetByLocationIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(partner);
        }

        [Fact]
        public async Task WhenDeleteAsyncIsCalled_ThenPartnerIsDeleted()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
                .SetupDeleteAction();
            var partnerId = Guid.NewGuid();

            // Act
            await fixture.PartnerService.DeleteAsync(partnerId);

            // Assert
            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.DeleteAsync(It.IsAny<string>()), Times.Exactly(2));

            fixture.PartnerRepositoryMock
                .Verify(p => p.DeleteAsync(It.Is<Guid>(g => g == partnerId)),Times.Once);
        }

        [Fact]
        public async Task WhenDeleteAsyncIsCalled_ThenPartnerNoPartnerIsDeleted()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
                {
                    Partner = null
                }.SetupDeleteAction();
            var partnerId = Guid.NewGuid();

            // Act
            await fixture.PartnerService.DeleteAsync(partnerId);

            // Assert
            fixture.CustomerProfileClientMock
                .Verify(p => p.PartnerContact.DeleteAsync(It.IsAny<string>()), Times.Never);

            fixture.PartnerRepositoryMock
                .Verify(p => p.DeleteAsync(It.Is<Guid>(g => g == partnerId)), Times.Never);
        }

        [Fact]
        public async Task WhenCreateAsyncIsCalled_ThenPartnerIsCreated()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partner = new Partner
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
                        }
                    }
                }
            }.SetupCreateAction();

            var partner = fixture.Partner;

            // Act
            await fixture.PartnerService.CreateAsync(partner);

            // Assert
            fixture.LocationServiceMock
                .Verify(p => p.CreateLocationsContactPersonForPartnerAsync(It.IsAny<Partner>()), Times.Once);

            fixture.CredentialsClientMock
                .Verify(p => p.Partners.CreateAsync(It.Is<PartnerCredentialsCreateRequest>(c => 
                        c.ClientId == partner.ClientId && c.ClientSecret == partner.ClientSecret)));

            fixture.PartnerCreatedPublisherMock.Verify(x => x.PublishAsync(It.IsAny<PartnerCreatedEvent>()));
        }

        [Fact]
        public async Task WhenCreateAsyncIsCalled_ThenPartnerLoginAlreadyExists()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                CredentialsCreateResponse = new CredentialsCreateResponse
                {
                    Error = CredentialsError.LoginAlreadyExists
                }
            }.SetupCreateAction();

            var partner = fixture.Partner;

            // Act
            // Assert
            await Assert.ThrowsAsync<ClientAlreadyExistException>(() => 
                fixture.PartnerService.CreateAsync(partner));
        }

        [Fact]
        public async Task WhenCreateAsyncIsCalled_ThenPartnerRegistrationFails()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                CredentialsCreateResponse = new CredentialsCreateResponse
                {
                    // Random error different than LoginAlreadyExist
                    Error = CredentialsError.LoginNotFound
                }
            }.SetupCreateAction();

            var partner = fixture.Partner;

            // Act
            // Assert
            await Assert.ThrowsAsync<PartnerRegistrationFailedException>(() => 
                fixture.PartnerService.CreateAsync(partner));
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenPartnerIsUpdated()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partner = new Partner
                {
                    Id = Guid.NewGuid(),
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
                        }
                    }
                }
            }.SetupUpdateAction();

            var partner = fixture.Partner;

            // Act
            await fixture.PartnerService.UpdateAsync(partner);

            // Assert
            fixture.LocationServiceMock
                .Verify(p => p.UpdateRangeAsync(It.IsAny<Partner>(), It.IsAny<IReadOnlyList<Location>>(), It.IsAny<IReadOnlyList<Location>>()), Times.Once);

            fixture.CredentialsClientMock
                .Verify(p => p.Partners.UpdateAsync(It.Is<PartnerCredentialsUpdateRequest>(c =>
                    c.ClientId == partner.ClientId && c.ClientSecret == partner.ClientSecret)));
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenPartnerIsUpdatedButNotCredentials()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
                .SetupUpdateAction();

            var partner = fixture.Partner;
            partner.ClientSecret = null;

            // Act
            await fixture.PartnerService.UpdateAsync(partner);

            partner.ClientId = null;
            await fixture.PartnerService.UpdateAsync(partner);

            // Assert
            fixture.LocationServiceMock
                .Verify(p => p.UpdateRangeAsync(It.IsAny<Partner>(), It.IsAny<IReadOnlyList<Location>>(), It.IsAny<IReadOnlyList<Location>>()), Times.Exactly(2));

            fixture.CredentialsClientMock
                .Verify(p => p.Partners.UpdateAsync(It.IsAny<PartnerCredentialsUpdateRequest>()), Times.Never);
        }

        [Fact]
        public async Task WhenUpdateAsyncIsCalled_ThenPartnerThrowWhenCredentialsAreNotUpdated()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture
                {
                    CredentialsUpdateResponse = new CredentialsUpdateResponse
                    {
                        Error = CredentialsError.LoginNotFound
                    }
                }
                .SetupUpdateAction();

            var partner = fixture.Partner;
            partner.ClientSecret = "secret";
            
            // Act
            // Assert
            await Assert.ThrowsAsync<PartnerRegistrationUpdateFailedException>(() =>
                fixture.PartnerService.UpdateAsync(partner));
        }

        [Fact]
        public async Task WhenGetByIdsAsyncIsCalled_ThenListIsReturnedWithData()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
                .SetupGetActions();

            // Act
            var partners = await fixture.PartnerService.GetByIdsAsync(new Guid[1]);

            // Assert
            Assert.Equal(fixture.Partners.Item1, partners);
        }

        [Fact]
        public async Task WhenGetByIdsAsyncIsCalled_ThenListIsReturnedWithoutData()
        {
            // Arrange
            var fixture = new PartnerServiceTestsFixture()
            {
                Partners = (new List<Partner>(), 0)
            }.SetupGetActions();

            // Act
            var partners = await fixture.PartnerService.GetByIdsAsync(new Guid[0]);

            // Assert
            Assert.Equal(fixture.Partners.Item1, partners);
        }
    }
}
