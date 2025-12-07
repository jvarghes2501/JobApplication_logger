using Entities;
using System;
using System.Collections.Generic;


namespace ServiceContracts.DTO
{
    public class LocationAddRequest
    {
        public string ? LocationName { get; set; }
        public Location ToLocation()
        {
            return new Location() { LocationName = this.LocationName  };
        }
    }
}
