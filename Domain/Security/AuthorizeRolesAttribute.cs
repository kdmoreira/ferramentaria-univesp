using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Domain.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params RoleEnum[] roles) : base()
        {
            var rolesList = new List<string>();

            foreach (var role in roles)
            {
                rolesList.Add(role.GetDescription());
            }

            Roles = string.Join(",", rolesList);
        }
    }
}
