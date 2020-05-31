using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.DTO
{
    public class RoleDTO
    {
        public string RoleName { get; set; }
    }

    public class EditRoleDTO
    {
        public string UserId { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
