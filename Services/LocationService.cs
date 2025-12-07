using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class LocationService : ILocationService
    {
        private readonly List<Location> _locationDataStore; 

        public LocationService()
        {
            _locationDataStore = new List<Location>(); // Initialize the in-memory data store
        }
        public LocationResponse AddLocation(LocationAddRequest? locationAddRequest)
        {
            if (locationAddRequest == null) // Validate input
            {
                throw new ArgumentNullException(nameof(locationAddRequest), "LocationAddRequest cannot be null");
            }

            if (locationAddRequest.LocationName == null) // Validate LocationName
            {
                throw new ArgumentException("LocationName cannot be null", nameof(locationAddRequest.LocationName));
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
