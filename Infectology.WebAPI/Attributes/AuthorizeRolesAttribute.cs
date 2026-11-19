using Infectology.Library.Entities.Users;
using Microsoft.AspNetCore.Authorization;

namespace Infectology.WebAPI.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params Role[] roles) => Roles = string.Join(",", roles);
}
