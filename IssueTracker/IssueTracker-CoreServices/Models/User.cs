using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Models
{
    public class User : IdentityUser<Guid>
    {
       public RoleLevel Role { get; set; } 
    }
}
