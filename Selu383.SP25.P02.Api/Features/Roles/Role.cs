using Microsoft.AspNetCore.Identity;

namespace Selu383.SP25.P02.Api.Features.Roles
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRole { get; set; }
    }
}
