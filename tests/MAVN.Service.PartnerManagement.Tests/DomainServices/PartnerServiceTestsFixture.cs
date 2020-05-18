using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Logs;
using MAVN.Service.Credentials.Client;
using MAVN.Service.Credentials.Client.Models.Requests;
using MAVN.Service.Credentials.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Models.Dto;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PartnerManagement.DomainServices;
using Moq;
using Vertical = MAVN.Service.PartnerManagement.Domain.Models.Vertical;

namespace MAVN.Service.PartnerManagement.Tests.DomainServices
{
    public class PartnerServiceTestsFixture
    {
        public PartnerServiceTestsFixture()
        {
            var mapper = MapperHelper.CreateAutoMapper();

            PartnerRepositoryMock = new Mock<IPartnerRepository>(MockBehavior.Strict);
            LocationServiceMock = new Mock<ILocationService>(MockBehavior.Strict);
            CredentialsClientMock = new Mock<ICredentialsClient>(MockBehavior.Strict);
            CustomerProfileClientMock = new Mock<ICustomerProfileClient>(MockBehavior.Strict);
            LocationsRepositoryMock = new Mock<ILocationRepository>(MockBehavior.Strict);

            PartnerService = new PartnerService(
                PartnerRepositoryMock.Object,
                LocationServiceMock.Object,
                CredentialsClientMock.Object,
                CustomerProfileClientMock.Object,
                LocationsRepositoryMock.Object,
                mapper,
                EmptyLogFactory.Instance);

            Partner = new Partner
            {
                Id = Guid.NewGuid(),
                Name = "Partner 1",
                Description = "Partner 1 Desc",
                BusinessVertical = Vertical.Hospitality,
                ClientId = "clientId1",
                CreatedAt = DateTime.UtcNow,
                AmountInCurrency = 1,
                AmountInTokens = 2,
                Locations = new List<Location>
                {
                    new Location
                    {
                        Id = LocationId,
                        Name = "Holiday Inn",
                        Address = "Dubai",
                        CreatedAt = DateTime.UtcNow,
                        ContactPerson = null
                    },
                    new Location
                    {
                        Id = Guid.NewGuid(),
                        Name = "Holiday Relax",
                        Address = "Bahri",
                        CreatedAt = DateTime.UtcNow,
                        ContactPerson = null
                    }
                }
            };

            PartnerContactResponse = new PartnerContactResponse
            {
                PartnerContact = new PartnerContact
                {
                    FirstName = "Owner",
                    LastName = "Johnson",
                    Email = "mail@mail.com",
                    PhoneNumber = "123-123-123",
                    LocationId = LocationId.ToString()
                }
            };

            Partners = (new List<Partner>
            {
                Partner
            }.AsReadOnly(), 1);
        }

        public PartnerService PartnerService { get; set; }

        public Mock<ICustomerProfileClient> CustomerProfileClientMock { get; set; }

        public Mock<ICredentialsClient> CredentialsClientMock { get; set; }

        public Mock<ILocationService> LocationServiceMock { get; set; }

        public Mock<IPartnerRepository> PartnerRepositoryMock { get; set; }
        public Mock<ILocationRepository> LocationsRepositoryMock { get; set; }

        public (IReadOnlyCollection<Partner>, int) Partners { get; set; }

        public Guid LocationId { get; set; }

        public PartnerContactResponse PartnerContactResponse { get; set; }

        public CredentialsCreateResponse CredentialsCreateResponse { get; set; } = new CredentialsCreateResponse
        {
            Error = CredentialsError.None
        };
        public CredentialsUpdateResponse CredentialsUpdateResponse { get; set; } = new CredentialsUpdateResponse
        {
            Error = CredentialsError.None
        };

        public Partner Partner { get; set; }

        public PartnerServiceTestsFixture SetupGetActions()
        {
            PartnerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<PartnerListRequestDto>()))
                .ReturnsAsync(() => Partners);

            PartnerRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => Partner);

            PartnerRepositoryMock.Setup(m => m.GetByClientIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => Partner);

            PartnerRepositoryMock.Setup(m => m.GetByLocationIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => Partner);

            CustomerProfileClientMock.Setup(m => m.PartnerContact.GetByLocationIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => PartnerContactResponse);

            PartnerRepositoryMock.Setup(m => m.GetByIdsAsync(It.IsAny<Guid[]>()))
                .ReturnsAsync(() => Partners.Item1);

            return this;
        }

        public PartnerServiceTestsFixture SetupDeleteAction()
        {
            PartnerRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => Partner);

            CustomerProfileClientMock.Setup(m => m.PartnerContact.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            PartnerRepositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            return this;
        }

        public PartnerServiceTestsFixture SetupCreateAction()
        {
            LocationServiceMock.Setup(m => m.CreateLocationsContactPersonForPartnerAsync(It.IsAny<Partner>()))
                .ReturnsAsync(() => Partner.Locations);

            PartnerRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(() => Partner);

            CredentialsClientMock.Setup(m => m.Partners.CreateAsync(It.IsAny<PartnerCredentialsCreateRequest>()))
                .ReturnsAsync(() => CredentialsCreateResponse);

            return this;
        }

        public PartnerServiceTestsFixture SetupUpdateAction()
        {
            PartnerRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => Partner);

            LocationServiceMock.Setup(m => m.UpdateRangeAsync(It.IsAny<Partner>(), It.IsAny<IReadOnlyList<Location>>(), It.IsAny<IReadOnlyList<Location>>()))
                .ReturnsAsync(() => Partner.Locations);

            CredentialsClientMock.Setup(m => m.Partners.UpdateAsync(It.IsAny<PartnerCredentialsUpdateRequest>()))
                .ReturnsAsync(() => CredentialsUpdateResponse);

            PartnerRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Partner>()))
                .Returns(Task.CompletedTask);

            return this;
        }
    }
}
