using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class LocationService : ILocationService
    {
        private readonly List<Location> _locationDataStore; 

        public LocationService(bool init = true)
        {
            _locationDataStore = new List<Location>(); // Initialize the in-memory data store

            if (init)
            {
                _locationDataStore.AddRange(new List<Location>() {
                new Location() { LocationID = Guid.Parse("41BCA4C5-F4DB-4647-9142-42B06EF582F5"), LocationName = "New York"},
                new Location() { LocationID = Guid.Parse("7566EE69-8250-4902-B378-76E6638D2E4A"), LocationName = "San Francisco"},
                new Location() { LocationID = Guid.Parse("262EFDBD-0874-494B-BFAD-754D723B07E8"), LocationName = "Chicago"},
                new Location() { LocationID = Guid.Parse("E9A724B2-E696-434B-827A-4C675C6B0AAA"), LocationName = "Seattle"},
                new Location() { LocationID = Guid.Parse("FA38ACD7-19E9-4695-A5A7-6BAA24BB9EBF"), LocationName = "Austin"}
                });
            }
        }
        public LocationResponse AddLocation(LocationAddRequest? locationAddRequest)
        {
            if (locationAddRequest == null) // Validate input
            {
                throw new ArgumentNullException(nameof(locationAddRequest), "LocationAddRequest cannot be null");
            }

            if (locationAddRequest.LocationName == null) // Validate LocationName
            {
                throw new ArgumentNullException("LocationName cannot be null", nameof(locationAddRequest.LocationName));
            }

            if (_locationDataStore.Where(loc => loc.LocationName == locationAddRequest.LocationName).Count() > 0) // Check for duplicate LocationName
            {
                throw new ArgumentException($"Location with name {locationAddRequest.LocationName} already exists", nameof(locationAddRequest.LocationName));
            }

            Location newLocation = locationAddRequest.ToLocation(); // Convert to Location entity
            newLocation.LocationID = Guid.NewGuid(); // Assign a new GUID
            _locationDataStore.Add(newLocation); // Add to the in-memory data store

            return newLocation.ToLocationResponse();// Convert to LocationResponse and return
        }

        public List<LocationResponse> GetAllLocations()
        {
            return _locationDataStore.Select(loc => loc.ToLocationResponse()).ToList(); // Convert all Location entities to LocationResponse and return
        }

        public LocationResponse? GetLocationbyLocationId(Guid? locationId)
        {
            if (locationId == null) // Validate input
            {
                throw new ArgumentNullException(nameof(locationId), "LocationId cannot be null");
            }

            Location? location = _locationDataStore.FirstOrDefault(loc => loc.LocationID == locationId); // Find the location by ID

            return location?.ToLocationResponse(); // Convert to LocationResponse and return, or null if not found
        }
    }
}
