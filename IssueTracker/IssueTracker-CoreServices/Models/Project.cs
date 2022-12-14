using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string VersionControlURL { get; set; }
        public List<Issue> Issues { get; set; } = new List<Issue>();
        public List<IssueTrackerUser> Maintainers{ get; set; } = new List<IssueTrackerUser>();
    }
}
