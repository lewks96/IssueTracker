using IssueTracker_CoreServices.Data;
using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Service
{
    public class IssueService : IServiceBase<Issue, int>
    {
        public IssueService(ApplicationDbContext context) : base(context, context.IssuesDb)
        {
        }
    }
}
