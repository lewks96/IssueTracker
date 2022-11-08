using IssueTracker_CoreServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IssueTracker_CoreServices.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-IssueTracker;Trusted_Connection=True;MultipleActiveResultSets=true");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        public DbSet<User> UsersDb { get; set; }
    }
}