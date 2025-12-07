using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ILocationService
    {
        /*
         * Adds a new location based on the provided LocationAddRequest object to the data store.
         */
        LocationResponse AddLocation(LocationAddRequest? locationAddRequest);

        /*
         * Retrieves all locations from the data store and returns them as a list of LocationResponse objects.
         */
        List<LocationResponse> GetAllLocations();

        /*
         * Retrieves a specific location by its unique identifier (locationId) from the data store.
         */
        LocationResponse? GetLocationbyLocationId(Guid? locationId);
    }
}
