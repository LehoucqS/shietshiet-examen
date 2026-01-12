using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.API.Contracts
{
    public class LocationPostRequest
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public LocationPostRequest(string name, string description, double latitude, double longitude)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
