using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Models
{
    public enum IssueStatus
    {
        Open,
        Review,
        Closed,
    }

    public class Issue
    {
        public int Id { get; set; } 
        public string Name { get; set; }  
        public string Description { get; set; }  
        public IssueStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? ClosedDate { get; set; }
        public IEnumerable<IssueTrackerUser> Assignees { get; set; }
    }
}
