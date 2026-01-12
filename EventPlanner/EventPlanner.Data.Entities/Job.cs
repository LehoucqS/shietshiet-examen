namespace EventPlanner.Data.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public Event EventAssigned { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime DeadLineDateTime { get; set; }
    }
}
