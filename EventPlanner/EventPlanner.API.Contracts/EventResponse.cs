using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.API.Contracts
{
    public record EventResponse(int Id, string Location, string Name, DateTime StartDateTime, DateTime EndDateTime)
    {
    }
}
