using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class DataContextEF : DbContext
    {
        public string connectionString = "Server=SherifAbdullah\\MSSQLSERVER01;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // Add you Models Here
        public DbSet<User> Users { get; set; }
        public DbSet<UserSalary> UserSalaries { get; set; }
        public DbSet<UserJobInfo> UserJobInfos { get; set; }

        
        // Model Creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("SchemaName"); // Schema name
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.UserId);
            modelBuilder.Entity<UserSalary>().ToTable("UserSalary").HasKey(u => u.UserId);
            modelBuilder.Entity<UserJobInfo>().ToTable("UserJobInfo").HasKey(u => u.UserId);
        }

        // Configurations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If optionsBuilder is not Configured.
            if(!optionsBuilder.IsConfigured) {
                    optionsBuilder.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

    }
}