using IssueTracker_CoreServices.Data;
using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Service
{
    public class ProjectService : IServiceBase<ApplicationDbContext, Project, int>
    {
        public ProjectService(ApplicationDbContext context) : base(context, context.ProjectsDb)
        {
        }
    }
}
