namespace EventPlanner.Service.Models
{
    public class Location
    {
        private string _name;
        private double _longitude;
        private double _latitude;

        public Location(int id, string name, string description, double longitude, double latitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
        }

        public Location(string name, string description, double longitude, double latitude)
        {
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;

        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Name));
                _name = value;
            }
        }

        public double Longitude
        {
            get => _longitude;
            set
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 180, nameof(Longitude));
                ArgumentOutOfRangeException.ThrowIfLessThan(value, -180, nameof(Longitude));
                _longitude = value;
            }
        }

        public double Latitude
        {
            get => _latitude;
            set
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 180, nameof(Latitude));
                ArgumentOutOfRangeException.ThrowIfLessThan(value, -180, nameof(Latitude));
                _latitude = value;
            }
        }
    }
}
