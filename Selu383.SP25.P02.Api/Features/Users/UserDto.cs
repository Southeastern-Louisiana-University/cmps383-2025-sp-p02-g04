using Selu383.SP25.P02.Api.Features.Roles;
using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.P02.Api.Features.Users
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Password { get; set; }
        public required List<string> Roles { get; set; }
    }

    public class UserGetDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required List<String> Roles { get; set; }
    }
}
