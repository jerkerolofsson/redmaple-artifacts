using RedMaple.Artifacts.ApiService.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ArtifactsDbContext>("db", (configure) =>
{
    var runner = new MigrationsRunner(configure.ConnectionString!);
    runner.RunUp();
});

//builder.Services.AddDbContext<ArtifactsDbContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseNpgsql());
builder.Services.AddScoped<IArtifactsManager, ArtifactsManager>();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
