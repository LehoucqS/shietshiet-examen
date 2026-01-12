using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Service.Models
{
    public class Report
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public Report(int eventId, string eventName, int locationId, string locationName)
        {
            EventId = eventId;
            EventName = eventName;
            LocationId = locationId;
            LocationName = locationName;
        }
    }
}
