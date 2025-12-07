using System;
using Entities;

namespace ServiceContracts.DTO
{
    public class LocationResponse
    {
        public Guid locationId { get; set; }
        public string? locationName { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(LocationResponse))
            {
                return false;
            }
            LocationResponse loc_to_check = (LocationResponse)obj;
            return (this.locationId == loc_to_check.locationId
                && this.locationName == loc_to_check.locationName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class LocationResponseExtensions
    {
        public static LocationResponse ToLocationResponse(this Location location)
        {
            return new LocationResponse()
            {
                locationId = location.LocationID,
                locationName = location.LocationName
            };
        }
    }
}
