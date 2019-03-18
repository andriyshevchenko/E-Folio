using Microsoft.AspNetCore.Identity;

namespace eFolio.EF
{
    public class UserEntity : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}