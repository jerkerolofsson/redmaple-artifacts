namespace RedMaple.Artifacts.ApiService.Infrastructure
{
    public class ArtifactsDbContext : DbContext
    {
        public DbSet<ArtifactDbo> Artifacts { get; set; }
        public DbSet<ArtifactVersionDbo> Versions { get; set; }
        public DbSet<ProductDbo> Products { get; set; }

        public ArtifactsDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtifactDbo>();
            modelBuilder.Entity<ArtifactVersionDbo>();
            modelBuilder.Entity<ProductDbo>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
