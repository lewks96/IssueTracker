using IssueTracker_CoreServices.Data;
using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Service;
using IssueTracker_Testing.Harness;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker_Testing.ServiceTests
{
    class ProjectSpecialization : IEntitySpecialization<Project>
    {
        public bool CompareEntity(Project entity1, Project entity2)
        {
            return entity1.Description == entity2.Description;
        }
        public Project ModifyEntity(Project e)
        {
            e.Description = "Hey";
            return e;
        }
    }
    public class ProjectServiceTests : ServiceLayerTestHarness<ApplicationDbContext, Project, int>
    {
        public override void OneTimeSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-IssueTracker").Options;

            _context = new ApplicationDbContext(options);
            _sut = new ProjectService(_context);
            _entitySpecialization = new ProjectSpecialization();
        }

        protected override int GetPerTestRandomSeed()
        {
            return new Random().Next();
        }
    }
}
