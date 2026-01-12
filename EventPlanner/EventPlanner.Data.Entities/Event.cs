namespace EventPlanner.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public Location EventLocation { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<Job>? AssignedJobs { get; set; }
    }
}