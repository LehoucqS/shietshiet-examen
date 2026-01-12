using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.API.Contracts
{
    public record EventRequest(string Location, string Name, DateTime StartDateTime, DateTime EndDateTime)
    {
    }
}
