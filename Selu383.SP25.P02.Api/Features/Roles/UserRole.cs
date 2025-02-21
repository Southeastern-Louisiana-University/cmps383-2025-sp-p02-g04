using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.Users;

namespace Selu383.SP25.P02.Api.Features.Roles
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
