using FluentMigrator.Runner;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;

namespace RedMaple.Artifacts.ApiService.Infrastructure.Migrations
{
    public class MigrationsRunner
    {
        private string? mConnectionString;

        public MigrationsRunner(string connectionString)
        {
            mConnectionString = connectionString;
        }

        public IMigrationRunner CreateRunner()
        {
            var services = new ServiceCollection();
            services.AddDbContext<DevArtifactsDbContext>(options =>
            {
                options.UseNpgsql(mConnectionString);
            });
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(mConnectionString)
                .WithGlobalCommandTimeout(TimeSpan.FromMinutes(30))
                .ScanIn(typeof(MigrationsRunner).Assembly).For.Migrations());

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DevArtifactsDbContext>();
            dbContext.Database.EnsureCreated(); 

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            return runner;
        }

        public void RunUp()
        {
            var startTimestamp = Stopwatch.GetTimestamp();
            while (true)
            {

                try
                {
                    var runner = CreateRunner();
                    runner.MigrateUp();
                    return;
                }
                catch (Exception ex)
                {
                    var elapsed = Stopwatch.GetElapsedTime(startTimestamp);
                    if (elapsed > TimeSpan.FromMinutes(10))
                    {
                        throw new Exception("Failed to run migrations", ex);
                    }
                }
            }
        }
    }
}
