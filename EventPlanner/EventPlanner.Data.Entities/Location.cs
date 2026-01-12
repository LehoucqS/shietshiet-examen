namespace EventPlanner.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double GpsLat { get; set; }
        public double GpsLon { get; set; }
    }
}
