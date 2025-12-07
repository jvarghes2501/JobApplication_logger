using ServiceContracts;
using Xunit;
using ServiceContracts;
using Services;
using ServiceContracts.DTO;

namespace CRUDTests
{
    public class LocationServiceTests
    {
        private readonly ILocationService _locationService;

        public LocationServiceTests()
        {
            // Initialize the LocationService before each test
            _locationService = new LocationService();
        }

        #region AddLocation

        [Fact]
        public void addLocation_NullLocation()
        {
            LocationAddRequest? locationAddRequest = null;
            Assert.Throws<ArgumentNullException>(() => _locationService.AddLocation(locationAddRequest));
        }

        [Fact]
        public void addLocation_NullLocationName()
        {
            LocationAddRequest? locationAddRequest = new LocationAddRequest() { LocationName = null};
            Assert.Throws<ArgumentNullException>(() => _locationService.AddLocation(locationAddRequest));
        }

        [Fact]
        public void addLocation_DuplicateLocation()
        {
           LocationAddRequest? locationAddRequest1 = new LocationAddRequest() { LocationName = "Hamilton" };
           LocationAddRequest? locationAddRequest2 = new LocationAddRequest() { LocationName = "Hamilton" };

          Assert.Throws<ArgumentException> (() => 
          {
              _locationService.AddLocation(locationAddRequest1);
              _locationService.AddLocation(locationAddRequest2);
          });
        }

        [Fact]
        public void addLocation_CorrectLocationDetails()
        {
            LocationAddRequest? locationAddRequest = new LocationAddRequest() { LocationName = "Toronto" };

            LocationResponse locationResponse = _locationService.AddLocation(locationAddRequest);
            List<LocationResponse> allLocations = _locationService.GetAllLocations();

            Assert.True(locationResponse.locationId != Guid.Empty);
            Assert.Contains(locationResponse, allLocations);
        }

        #endregion

        #region GetAllLocations
        [Fact]
        public void getAllLocations_EmptyList()
        {
            List<LocationResponse> allLocations = _locationService.GetAllLocations();
            Assert.Empty(allLocations);
        }


        [Fact]
        public void getAllLocations_AddLocations()
        {
            List<LocationAddRequest> locationAddRequests = new List<LocationAddRequest>()
            {
                new LocationAddRequest() { LocationName = "Hamilton" },
                new LocationAddRequest() { LocationName = "Toronto" },
                new LocationAddRequest() { LocationName = "Missisauga" }
            };

            List<LocationResponse> addedLocations = new List<LocationResponse>();

            foreach (var locationAddRequest in locationAddRequests)
            {
                LocationResponse locationResponse = _locationService.AddLocation(locationAddRequest);
                addedLocations.Add(locationResponse);
            }

            List<LocationResponse> allLocations_actual = _locationService.GetAllLocations();

           foreach (LocationResponse location in addedLocations)
           {
                Assert.Contains(location, allLocations_actual);
            }
        }
        #endregion

        #region GetLocationByLocationId

        [Fact]
        public void getLocationByLocationId_NullLocationId()
        {
            Guid? locationId = null;
            Assert.Throws<ArgumentNullException>(() => _locationService.GetLocationbyLocationId(locationId));
        }

        [Fact]
        public void getLocationByLocationId_CorredId()
        {
           LocationAddRequest? locationAddRequest = new LocationAddRequest() { LocationName = "Vancouver" };
           LocationResponse locationResponse_added = _locationService.AddLocation(locationAddRequest);

           LocationResponse? locationResponse_fetched = _locationService.GetLocationbyLocationId(locationResponse_added.locationId);
           Assert.Equal(locationResponse_added, locationResponse_fetched);
        }
        #endregion
    }
}