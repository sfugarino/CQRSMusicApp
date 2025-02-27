using Microsoft.EntityFrameworkCore;
using Music.Persistence;
using Music.Domain.Repositories;
using Music.Persistence.Options;
using Music.Infrastructure;
using Microsoft.Extensions.Options;
using Scrutor;
using Music.Persistence.Repositories;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddDbContext<ApplicationDbContext>(
    (serviceProvider, dbContextOptionsBuilder) =>
{
    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()?.Value;

    var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
    var connectionString = databaseOptions?.ConnectionString ?? string.Empty;
    connectionString = string.Format(connectionString, password);

    dbContextOptionsBuilder.UseSqlServer(connectionString, sqlServerActions =>
    {
        sqlServerActions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
    });

    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions?.EnableSensitiveDataLogging ?? false);
    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions?.EnableDetailedErrors ?? true);

});

builder.Services.Scan(scan => scan
    .FromAssemblies(
    Music.Infrastructure.AssemblyReference.Assembly)
    .AddClasses(false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Music.Application.AssemblyReference.Assembly));

builder.Services.
    AddControllers()
    .AddApplicationPart(Music.Presentation.AssemblyReference.Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379"; // redis is the container name of the redis service. 6379 is the default port
    options.InstanceName = "SampleInstance";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    using (var context = scope?.ServiceProvider.GetRequiredService<ApplicationDbContext>())
    {
        if (context is not null)
        {
            context.Database.Migrate();
            DbInitializer.Initialize(context);
        }

    }
}

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
};
app.Run();
