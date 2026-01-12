# API SLN VLGS LESSEN NAJAAR 2025

Voorbeeld: EventPlanner met opgave uit cursus (Cover All)

## Opzetten Project

1. New Blank Solution
2. Add Project -> EventPlanner.API        (ASP.NET Core Web API)
3. Add Project -> EventPlanner.API.Contracts    (Class Library)
4. Add Project -> EventPlanner.Data        (Class Library)
5. Add Project -> EventPlanner.Data.Entities    (Class Library)
6. Add Project -> EventPlanner.Service        (Class Library)
7. Add Project -> EventPlanner.Service.Models    (Class Library)
8. Add Nuget Packages (op heel solution voor 't gemak)
    1. Npgsql.EntityFrameworkCore.PostgreSQL
    2. Microsoft.EntityFrameworkCore.Design
    3. Microsoft.AspNetCore.OpenApi
    4. Dapper
    5. MongoDB.Driver

## Aanmaak DB Postgres

### !! DOCKER OPSTARTEN !!

1. Opzetten: 

    ```
    docker run --name {naam} -d -p 5432:5432 -e POSTGRES_PASSWORD={paswoord} postgres
    ; hier dus:
    docker run --name eventplanner -d -p 5432:5432 -e POSTGRES_PASSWORD=eventplannerpassword postgres
    ```

2. Testen: 
   
    ```
    docker exec -it {name} sh    ; docker exec -it eventplanner
    su postgres
    psql
    SELECT 1;
    ```

## EF Core

1. Maak entities (in EventPlanner.Data.Entities)
   
    1. Location
        
        ```
        int Id;
        string Name;
        string Description;
        double GpsLat;
        double GpsLon;
        ```

    2. Event
        
        ```
        int Id;
        Location EventLocation;
        string Name;
        DateTime StartDateTime;
        DateTime EndDateTime;
        List<Job> AssignedJobs;
        ```

    3. Job
        
        ```
        int Id;
        Event EventAssigned;
        string Name;
        string Description;
        int Priority;
        int Status;
        DateTime DeadlineDateTime;
        ```

2. Maak EventDbContext (in EventPlanner.Data)
   
    1. Project reference in EventPlanner.Data naar EventPlanner.Data.Entities
    2. EventDbContext erft over van DbContext:
        
        ```
        public class EventDbContext : DbContext
        ```
    3. Constructor aanmaken:
        
        ```
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }
        // is later nodig om connection string uit appsettings.json te extraheren
        ```
    4. DbSets aanmaken: 
        
        ```
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        ```
    5. Connection string toevoegen: 
        
        In project EventPlanner.API, appsettings.json en appsettings.Development.json:
        ```
        "ConnectionStrings": {
            "EventPlannerPostgres": "Host=localhost;Port=5432;Username=postgres;Password=eventplannerpassword;Database=postgres"
        }
        ```
        In project EventPlanner.API, Program.cs:
        ```
        var connectionString = builder.Configuration.GetConnectionString("EventPlannerPostgres");
        builder.Services.AddDbContext<EventDbContext>(options => options.UseNpgsql(connectionString));
        // voor builder.Build(); 
        ```
    6. Testen door migration aan te maken:
        In powershell, folder van EventPlanner.API:
        ```
        dotnet ef migrations add test --project ..\EventPlanner.Data\EventPlanner.Data.csproj
        dotnet ef database update

3. (optioneel) Maak models aan in EnventPlanner.Service.Models. Dit is enkel nodig indien bijvoorbeeld range checks moeten gedaan worden, anders zijn DTO's OK, en kunnen rechtstreeks entities aangemaakt worden om DbContext aan te passen.

4. Maak Services aan in EventPlanner.Service (voorbeeld hieronder met LocationService)

    1. Project Reference naar EventPlanner.Data, EventPlanner.API.Contracts, EventPlanner.Service.Models.

    2. Maak klasse aan LocationService

    3. Voeg dbcontext toe via dependency injection:
        ```
        public class LocationService
        {
            private readonly EventDbContext _context;

            private LocationService (EventDbContext context)
            {
                _context = context;
            }
        }
        ```
    
    4. Voorbeeld: GetAll
        ```
        public async Task<IEnumerable<LocationResponse>> GetAll()
        {
            List<Data.Entities.Location> locations = await _context.Locations.ToListAsync();
            IEnumerable<LocationResponse> result = locations.Select(l => new LocationResponse(l.Name, l.Description, l.GpsLat, l.GpsLon));
            return result;
        }
        ```
5. API deel (verderwerken op voorbeeld van Locations)

    1. Voeg toe in Program.cs:
        ```
        builder.Services.AddTransient<LocationService>();
        ```
    
    2. Maak Controller aan in Controllers folder (LocationController.cs)

    3. LocationService toevoegen met dependency injection
        ```
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }
        ```

## Voorbeelden van endpoints: bekijk solution


## MongoDB

    1. In Data, maak MongoDbSettings.cs
        ```
        public class MongoDbSettings
        {
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
            public string CollectionName { get; set; }
        }
        ```

    2. Voeg toe in appsettings.json (en development!)
        ```
        "MongoDbSettings": {
            "ConnectionString": "mongodb://localhost:27017",
            "DatabaseName": "eventaudit",
            "CollectionName": "audits"
            }
        ```

    3. Maak nieuwe service aan (AuditService.cs)
        Zie solution
    
    4. Database configureren en service registreren in program.cs
        ```
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
        builder.Services.AddSingleton<AuditService>();
        ```
    
    5. Zie solution voor gebruik

## Dapper
    1. Zie solution voor gebruik. Nog geen idee hoe te gebruiken met web API, waarschijnlijk ook met addtransient.
