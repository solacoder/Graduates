using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Status { set; get; }
        public string DeleteFlag { set; get; }
        public string ActionType { set; get; }
    }
}
