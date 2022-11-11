using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Models.DTO
{
    public class ProjectIssuesDto
    {
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public IEnumerable<Issue> Issues { get; set; } = new List<Issue>();

        public static ProjectIssuesDto CreateDto(Project project)
        {
            return new ProjectIssuesDto
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                Issues = project.Issues,
            };
        }
    }
}
