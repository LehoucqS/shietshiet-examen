namespace EventPlanner.Service.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Location EventLocation { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Event(int id, Location eventLocation, string name, DateTime startDateTime, DateTime endDateTime)
        {
            Id = id;
            EventLocation = eventLocation;
            Name = name;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
