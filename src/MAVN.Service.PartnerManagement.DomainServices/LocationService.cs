using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Geohash;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.PartnerManagement.Domain.Exceptions;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.Domain.Services;
using MoreLinq;

namespace MAVN.Service.PartnerManagement.DomainServices
{
    public class LocationService : ILocationService
    {
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly IGeocodingReader _geocodingReader;
        private readonly ILocationRepository _locationRepository;
        private readonly Geohasher _geohasher = new Geohasher();
        private readonly ILog _log;

        public LocationService(
            ICustomerProfileClient customerProfileClient,
            ILogFactory logFactory,
            ILocationRepository locationRepository,
            IGeocodingReader geocodingReader)
        {
            _customerProfileClient = customerProfileClient;
            _geocodingReader = geocodingReader;
            _locationRepository = locationRepository;
            _log = logFactory.CreateLog(this);
        }

        public Task<Location> GetByIdAsync(Guid id)
        {
            return _locationRepository.GetByIdAsync(id);
        }

        public Task<Location> GetByExternalIdAsync(string externalId)
        {
            return _locationRepository.GetByExternalIdAsync(externalId);
        }

        public async Task CreateLocationsContactPersonForPartnerAsync(Partner partner)
        {
            var customerProfileCreateActions = new List<Task>();

            if (await _locationRepository.AreExternalIdsNotUniqueAsync(partner.Id,
                partner.Locations.Select(l => l.ExternalId)))
            {
                throw new LocationExternalIdNotUniqueException("Not all locations external ids are unique.");
            }

            // We don't want 3 created by on the request side of things so we are setting it here
            foreach (var location in partner.Locations)
            {
                location.Id = Guid.NewGuid();
                location.CreatedBy = partner.CreatedBy;
                SetGeohash(location);
                await SetCountryIso3Code(location);

                _log.Info("Location creating", context: $"location: {location.ToJson()}");

                if(location.ContactPerson != null && !string.IsNullOrEmpty(location.ContactPerson.Email))
                    customerProfileCreateActions.Add(CreateOrUpdatePartnerContact(location));
            }

            await Task.WhenAll(customerProfileCreateActions);
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

            var deleteActions = new List<Task>();
            var customerProfileUpdateActions = new List<Task>();
            var customerProfileCreateActions = new List<Task>();

            // TODO: Add transaction
            if (deletedLocations.Any())
            {
                deletedLocations.ForEach(location =>
                {
                    _log.Info("Location deleting", context: $"location: {location.ToJson()}");

                    deleteActions.Add(_customerProfileClient.PartnerContact.DeleteIfExistAsync(location.Id.ToString()));
                });
            }

            if (updatedLocations.Any())
            {
                foreach (var location in updatedLocations)
                {
                    var existingLocation = existingLocations.First(p => p.Id == location.Id);
                    location.CreatedBy = existingLocation.CreatedBy;
                    location.CreatedAt = existingLocation.CreatedAt;
                    SetGeohash(location);
                    await SetCountryIso3Code(location);

                    _log.Info("Location updating", context: $"location: {location.ToJson()}");
                    if (location.ContactPerson != null && !string.IsNullOrEmpty(location.ContactPerson.Email))
                        customerProfileUpdateActions.Add(CreateOrUpdatePartnerContact(location));
                    else
                        deleteActions.Add(_customerProfileClient.PartnerContact.DeleteIfExistAsync(location.Id.ToString()));
                }
            }

            if (createdLocations.Any())
            {
                foreach (var location in createdLocations)
                {
                    location.Id = Guid.NewGuid();
                    location.CreatedBy = partner.CreatedBy;
                    SetGeohash(location);
                    await SetCountryIso3Code(location);

                    _log.Info("Location creating", context: $"location: {location.ToJson()}");

                    if (location.ContactPerson != null && !string.IsNullOrEmpty(location.ContactPerson.Email))
                        customerProfileCreateActions.Add(CreateOrUpdatePartnerContact(location));
                }
            }

            await Task.WhenAll(customerProfileUpdateActions);
            await Task.WhenAll(customerProfileCreateActions);
            await Task.WhenAll(deleteActions);

            var processedLocations = new List<Location>();

            processedLocations.AddRange(createdLocations);
            processedLocations.AddRange(updatedLocations);

            return processedLocations;
        }

        public Task<IReadOnlyList<string>> GetIso3CodesForLocations()
            => _locationRepository.GetIso3CodesForAllLocations();

        private void SetGeohash(Location location)
        {
            location.Geohash = IsCoordinatesDetermined(location)
                ? _geohasher.Encode(location.Latitude.Value, location.Longitude.Value, precision: 9)
                : null;
        }

        private async Task SetCountryIso3Code(Location location)
        {
            location.CountryIso3Code = IsCoordinatesDetermined(location)
                ? await _geocodingReader.GetCountryIso3CodeByCoordinateAsync(location.Latitude.Value, location.Longitude.Value)
                : null;
        }

        private bool IsCoordinatesDetermined(Location location)
        {
            return location?.Latitude != null && location?.Longitude != null;
        }

        private async Task CreateOrUpdatePartnerContact(Location location)
        {
            await _customerProfileClient.PartnerContact.CreateOrUpdateAsync(
                new PartnerContactRequestModel
                {
                    LocationId = location.Id.ToString(),
                    FirstName = location.ContactPerson.FirstName,
                    LastName = location.ContactPerson.LastName,
                    Email = location.ContactPerson.Email.ToLower(),
                    PhoneNumber = location.ContactPerson.PhoneNumber
                });
        }
    }
}
