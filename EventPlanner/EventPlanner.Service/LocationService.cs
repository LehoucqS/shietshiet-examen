using EventPlanner.API.Contracts;
using EventPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Service
{
    public class LocationService
    {
        private readonly EventDbContext _context;

        public LocationService (EventDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationResponse>> GetAll()
        {
            List<Data.Entities.Location> locations = await _context.Locations.ToListAsync();
            IEnumerable<LocationResponse> result = locations.Select(l => new LocationResponse(l.Name, l.Description, l.GpsLat, l.GpsLon));
            return result;
        }

        public async Task<LocationResponse> GetById(int id)
        {
            Data.Entities.Location? location = await _context.Locations.FindAsync(id);
            LocationResponse result = new LocationResponse(location.Name, location.Description, location.GpsLat, location.GpsLon);
            return result;
        }

        public async Task AddLocation(LocationPostRequest request)
        {
            Models.Location newLocationModel = new(request.Name, request.Description, request.Longitude, request.Latitude);
            Data.Entities.Location newLocationEntity = new()
            {
                Name = newLocationModel.Name,
                Description = newLocationModel.Description,
                GpsLat = newLocationModel.Latitude,
                GpsLon = newLocationModel.Longitude
            };

            await _context.Locations.AddAsync(newLocationEntity);
            await _context.SaveChangesAsync();
        }
    }
}
