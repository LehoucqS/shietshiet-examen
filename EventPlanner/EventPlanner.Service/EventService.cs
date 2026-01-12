using EventPlanner.API.Contracts;
using EventPlanner.Data;
using EventPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Dapper;

namespace EventPlanner.Service
{
    public class EventService
    {
        private readonly EventDbContext _context;
        private readonly AuditService _auditService;
        private readonly string _connectionString;

        public EventService(EventDbContext context, AuditService auditService)
        {
            _context = context;
            _auditService = auditService;
            _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=eventplannerpassword;Database=postgres";
        }

        public async Task<IEnumerable<EventResponse>> GetAll()
        {
            List<Event> events = await _context.Events.Include(e => e.EventLocation).ToListAsync();
            await _auditService.AddAudit("read", "event", new List<string> { });
            return events.Select(e => new EventResponse(
                e.Id,
                e.EventLocation.Name,
                e.Name,
                e.StartDateTime,
                e.EndDateTime
                ));
        }

        public async Task AddEvent(EventRequest request)
        {
            Location? location = await _context.Locations.FirstOrDefaultAsync(l => l.Name == request.Location);

            if (location is null)
            {
                throw new DivideByZeroException();
            }

            Data.Entities.Event newEventEntity = new()
            {
                EventLocation = location,
                Name = request.Name,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
            };

            await _context.Events.AddAsync(newEventEntity);
            await _context.SaveChangesAsync();
            await AddToReporting(new Models.Report(newEventEntity.Id, newEventEntity.Name, location.Id, location.Name));
        }

        public async Task DeleteEvent(int id)
        {
            Location? location = await _context.Locations.FindAsync(id);
            if (location is not null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddToReporting(Models.Report report)
        {
            const string Query = "INSERT INTO report (eventid, eventname, locationid, locationname) " +
                "VALUES (@EventId, @EventName, @LocationId, @LocationName);";
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(Query, report);
        }
    }
}
