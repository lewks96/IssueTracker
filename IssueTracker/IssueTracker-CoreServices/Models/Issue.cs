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

    public enum Severity
    {
        Critical,
        Major,
        Minor,
    }

    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public IssueStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Project Project { get; set; }
        public Severity Severity { get; set; }
        public List<IssueTrackerUser> Assignees { get; set; }
    }
}
