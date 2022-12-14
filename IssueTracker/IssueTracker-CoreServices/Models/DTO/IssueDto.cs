using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Models.DTO
{
    public class IssueDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }
        public Severity Severity { get; set; }
        public static Issue ToBasicIssue(IssueDto dto)
        {
            return new Issue
            {
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status,
                Severity = dto.Severity,
            };
        }

        public static IssueDto FromIssue(Issue issue)
        {
            return new IssueDto
            {
                Name = issue.Name,
                Description = issue.Description,
                Status = issue.Status,
                Severity = issue.Severity,
            };
        }
    }
}
