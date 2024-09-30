namespace RedMaple.Artifacts.ApiService.Infrastructure
{
    /// <summary>
    /// DB context used for development just to create the database and test migrations
    /// </summary>
    public class DevArtifactsDbContext : DbContext
    {
       
        public DevArtifactsDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
