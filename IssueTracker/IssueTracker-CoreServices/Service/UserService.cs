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
    public class UsersService : IServiceBase<IssueTrackerUser, string>
    {
        public UsersService(ApplicationDbContext context) : base(context, context.UsersDb)
        {
        }
    }
}
