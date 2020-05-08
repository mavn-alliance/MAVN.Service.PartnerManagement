using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Logs;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.DomainServices;
using Moq;
using Vertical = MAVN.Service.PartnerManagement.Domain.Models.Vertical;

namespace MAVN.Service.PartnerManagement.Tests.DomainServices
{
    public class LocationServiceTestsFixture
    {
        public LocationServiceTestsFixture()
        {
            MapperHelper.CreateAutoMapper();

            CustomerProfileClientMock = new Mock<ICustomerProfileClient>(MockBehavior.Strict);
            LocationRepositoryMock = new Mock<ILocationRepository>();

            LocationService = new LocationService(
                CustomerProfileClientMock.Object,
                EmptyLogFactory.Instance,
                LocationRepositoryMock.Object);

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
                        Id = LocationIds[0],
                        Name = "Holiday Inn",
                        Address = "Dubai",
                        CreatedAt = DateTime.UtcNow,
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
                        Id = LocationIds[1],
                        Name = "Holiday Relax",
                        Address = "Bahri",
                        CreatedAt = DateTime.UtcNow,
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
                        Id = LocationIds[2],
                        Name = "Holiday Relax",
                        Address = "Bahri",
                        CreatedAt = DateTime.UtcNow,
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

            PartnerContactResponse = new PartnerContactResponse
            {
                PartnerContact = new PartnerContact
                {
                    FirstName = "Owner",
                    LastName = "Johnson",
                    Email = "mail@mail.com",
                    PhoneNumber = "123-123-123",
                    LocationId = LocationIds[0].ToString()
                }
            };

            Partners = (new List<Partner>
            {
                Partner
            }.AsReadOnly(), 1);

            Location = new Location
            {
                Id = LocationIds[2],
                Name = "Holiday Relax",
                Address = "Bahri",
                CreatedAt = DateTime.UtcNow,
                ContactPerson = new ContactPerson
                {
                    FirstName = "Name 1",
                    LastName = "Name 2",
                    Email = "Email",
                    PhoneNumber = "Phone Number"
                }
            };
        }

        public LocationService LocationService { get; set; }

        public Location Location { get; set; }

        public Mock<ICustomerProfileClient> CustomerProfileClientMock { get; set; }

        public Mock<ILocationRepository> LocationRepositoryMock { get; set; }

        public (IReadOnlyCollection<Partner>, int) Partners { get; set; }

        public List<Guid> LocationIds { get; set; } = new List<Guid>
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        public PartnerContactResponse PartnerContactResponse { get; set; }

        public Partner Partner { get; set; }

        public PartnerContactErrorCodes PartnerCreateContactErrorCodes { get; set; } = PartnerContactErrorCodes.None;

        public PartnerContactErrorCodes PartnerUpdateContactErrorCodes { get; set; } = PartnerContactErrorCodes.None;


        public LocationServiceTestsFixture SetupUpdateAction()
        {
            CustomerProfileClientMock.Setup(m => m.PartnerContact.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            CustomerProfileClientMock.Setup(m => m.PartnerContact.UpdateAsync(It.IsAny<PartnerContactUpdateRequestModel>()))
                .ReturnsAsync(() => PartnerUpdateContactErrorCodes);

            CustomerProfileClientMock.Setup(m => m.PartnerContact.CreateIfNotExistAsync(It.IsAny<PartnerContactRequestModel>()))
                .ReturnsAsync(() => PartnerCreateContactErrorCodes);

            return this;
        }

        public LocationServiceTestsFixture SetupCreateAction()
        {
            CustomerProfileClientMock.Setup(m => m.PartnerContact.CreateIfNotExistAsync(It.IsAny<PartnerContactRequestModel>()))
                .ReturnsAsync(() => PartnerCreateContactErrorCodes);

            return this;
        }
    }
}
