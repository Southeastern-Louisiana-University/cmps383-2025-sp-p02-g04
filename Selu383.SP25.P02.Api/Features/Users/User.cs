using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.Roles;
using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.P02.Api.Features.Users
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<UserRole> UserRoles { get; set; } = new();

    }
}
