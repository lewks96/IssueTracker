using IssueTracker_CoreServices.Data;
using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Service;
using IssueTracker_Testing.Harness;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker_Testing.ServiceTests
{
    class IssueSpecialization : IEntitySpecialization<Issue>
    {
        public bool CompareEntity(Issue entity1, Issue entity2)
        {
            return entity1.Description == entity2.Description;
        }
        public Issue ModifyEntity(Issue e)
        {
            e.Description = "Hey";
            return e;
        }
    }
    public class IssueServiceTests : ServiceLayerTestHarness<ApplicationDbContext, Issue, int>
    {
        public override void OneTimeSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-IssueTracker").Options;

            _context = new ApplicationDbContext(options);
            _sut = new IssueService(_context);
            _entitySpecialization = new IssueSpecialization();
        }

        protected override int GetPerTestRandomSeed()
        {
            return new Random().Next();
        }
    }
}
