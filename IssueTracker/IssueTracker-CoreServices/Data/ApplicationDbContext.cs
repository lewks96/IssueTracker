using IssueTracker_CoreServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IssueTracker_CoreServices.Data
{
    public class ApplicationDbContext : IdentityDbContext<TrackerUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        // Generates DbContext for ef migrations when using a class library
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory() + "/../Common/")
                    .AddJsonFile("appsettings.json")
                    .Build();
               
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("default"));
                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        public DbSet<TrackerUser> UsersDb { get; set; }
    }
}