using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.CustomerProfile.Client.Models.Enums;
using Lykke.Service.CustomerProfile.Client.Models.Requests;
using Lykke.Service.PartnerManagement.Domain.Exceptions;
using Lykke.Service.PartnerManagement.Domain.Models;
using Lykke.Service.PartnerManagement.Domain.Repositories;
using Lykke.Service.PartnerManagement.Domain.Services;
using MoreLinq;

namespace Lykke.Service.PartnerManagement.DomainServices
{
    public class LocationService : ILocationService
    {
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ILocationRepository _locationRepository;
        private readonly ILog _log;

        public LocationService(
            ICustomerProfileClient customerProfileClient,
            ILogFactory logFactory,
            ILocationRepository locationRepository)
        {
            _customerProfileClient = customerProfileClient;
            _locationRepository = locationRepository;
            _log = logFactory.CreateLog(this);
        }

        public Task<Location> GetByIdAsync(string id)
        {
            return _locationRepository.GetByIdAsync(id);
        }

        public Task<Location> GetByExternalIdAsync(string externalId)
        {
            return _locationRepository.GetByExternalIdAsync(externalId);
        }

        public async Task<IReadOnlyCollection<Location>> CreateLocationsContactPersonForPartnerAsync(Partner partner)
        {
            var customerProfileCreateActions =
                new List<Task<(PartnerContactErrorCodes ErrorCode, Location Location)>>();

            if (await _locationRepository.AreExternalIdsNotUniqueAsync(partner.Id,
                partner.Locations.Select(l => l.ExternalId)))
            {
                throw new LocationExternalIdNotUniqueException("Not all locations external ids are unique.");
            }

            // We don't want 3 created by on the request side of things so we are setting it here
            partner.Locations.ForEach(location =>
            {
                location.Id = Guid.NewGuid();
                location.CreatedBy = partner.CreatedBy;
                _log.Info("Location creating", context: $"location: {location.ToJson()}");

                customerProfileCreateActions.Add(CreatePartnerContact(location));
            });

            var createResult = await Task.WhenAll(customerProfileCreateActions);

            if (createResult.Any(r => r.ErrorCode != PartnerContactErrorCodes.None))
            {
                var exception =
                    new LocationContactRegistrationFailedException("Creating the contact person data failed.");
                _log.Error(exception, context: createResult);
                throw exception;
            }

            return createResult.Select(l => l.Location).ToList();
        }

        public async Task<IReadOnlyCollection<Location>> UpdateRangeAsync(Partner partner,
            IReadOnlyCollection<Location> locations,
            IReadOnlyCollection<Location> existingLocations)
        {
            var deletedLocations = existingLocations
                .Where(o => locations.All(l => l.Id != o.Id))
                .ToList();
            var createdLocations = new List<Location>();
            var updatedLocations = new List<Location>();

            if (await _locationRepository.AreExternalIdsNotUniqueAsync(partner.Id, locations.Select(l => l.ExternalId)))
                throw new LocationExternalIdNotUniqueException("Not all locations external identifiers are unique.");

            foreach (var location in locations)
            {
                if (location.Id == Guid.Empty || existingLocations.All(o => o.Id != location.Id))
                    createdLocations.Add(location);
                else
                    updatedLocations.Add(location);
            }

            var repositoryActions = new List<Task>();
            var customerProfileUpdateActions =
                new List<Task<(PartnerContactErrorCodes ErrorCode, Location Location)>>();
            var customerProfileCreateActions =
                new List<Task<(PartnerContactErrorCodes ErrorCode, Location Location)>>();

            // TODO: Add transaction
            if (deletedLocations.Any())
            {
                deletedLocations.ForEach(location =>
                {
                    _log.Info("Location deleting", context: $"location: {location.ToJson()}");

                    repositoryActions.Add(_customerProfileClient.PartnerContact.DeleteAsync(location.Id.ToString()));
                });
            }

            if (updatedLocations.Any())
            {
                updatedLocations.ForEach(location =>
                {
                    var existingLocation = existingLocations.First(p => p.Id == location.Id);
                    location.CreatedBy = existingLocation.CreatedBy;
                    location.CreatedAt = existingLocation.CreatedAt;

                    _log.Info("Location updating", context: $"location: {location.ToJson()}");

                    customerProfileUpdateActions.Add(UpdatePartnerContact(location));
                });
            }

            if (createdLocations.Any())
            {
                createdLocations.ForEach(location =>
                {
                    location.Id = Guid.NewGuid();
                    location.CreatedBy = partner.CreatedBy;

                    _log.Info("Location creating", context: $"location: {location.ToJson()}");

                    customerProfileCreateActions.Add(CreatePartnerContact(location));
                });
            }

            var updateResult = await Task.WhenAll(customerProfileUpdateActions);

            if (updateResult.Any(r => r.Item1 != PartnerContactErrorCodes.None))
            {
                var exception = new LocationContactUpdateFailedException("Updating the contact person data failed.");
                _log.Error(exception, context: updateResult);
                throw exception;
            }

            var createResult = await Task.WhenAll(customerProfileCreateActions);

            if (createResult.Any(r => r.Item1 != PartnerContactErrorCodes.None))
            {
                var exception =
                    new LocationContactRegistrationFailedException("Creating the Contact person data failed.");
                _log.Error(exception, context: createResult);
                throw exception;
            }

            await Task.WhenAll(repositoryActions);

            var processedLocations = new List<Location>();

            processedLocations.AddRange(updateResult.Select(r => r.Location));
            processedLocations.AddRange(createResult.Select(r => r.Location));

            return processedLocations;
        }

        private async Task<(PartnerContactErrorCodes, Location)> UpdatePartnerContact(Location location)
        {
            var result = await _customerProfileClient.PartnerContact.UpdateAsync(new PartnerContactUpdateRequestModel
            {
                LocationId = location.Id.ToString(),
                FirstName = location.ContactPerson.FirstName,
                LastName = location.ContactPerson.LastName,
                Email = location.ContactPerson.Email.ToLower(),
                PhoneNumber = location.ContactPerson.PhoneNumber
            });

            return (result, location);
        }

        private async Task<(PartnerContactErrorCodes, Location)> CreatePartnerContact(Location location)
        {
            var result = await _customerProfileClient.PartnerContact.CreateIfNotExistAsync(
                new PartnerContactRequestModel
                {
                    LocationId = location.Id.ToString(),
                    FirstName = location.ContactPerson.FirstName,
                    LastName = location.ContactPerson.LastName,
                    Email = location.ContactPerson.Email.ToLower(),
                    PhoneNumber = location.ContactPerson.PhoneNumber
                });

            return (result, location);
        }
    }
}
