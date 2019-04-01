using eFolio.EF;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace eFolio.API.Seeds
{
    public class ContextInitializerForAuth
    {
        public static async Task Initialize(AuthDBContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            context.Database.EnsureCreated();
 
            if (!context.Users.Any())
            {
                var user1 = new UserEntity
                {
                    AccessFailedCount = 0,
                    Email = "sashaburko000@gmail.com",
                    EmailConfirmed = true,
                    UserName = "sashaburko",
                    FirstName = "Oleksandr",
                    LastName = "Burko",
                };
                await userManager.CreateAsync(user1, "Pass1234@");

                var user2 = new UserEntity
                {
                    AccessFailedCount = 0,
                    Email = "ostaproik@gmail.com",
                    EmailConfirmed = true,
                    UserName = "ostaproik",
                    FirstName = "Ostap",
                    LastName = "Roik"
                };
                await userManager.CreateAsync(user2, "Pass1234@");

                var user3 = new UserEntity
                {
                    AccessFailedCount = 0,
                    Email = "yuralevko@gmail.com",
                    EmailConfirmed = true,
                    UserName = "yuralevko",
                    FirstName = "Yura",
                    LastName = "Levko"
                };

                var res = await userManager.CreateAsync(user3, "Pass1234@");

                await userManager.AddClaimAsync(user1, new System.Security.Claims.Claim("role", "user"));
                await userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("role", "user"));
                await userManager.AddClaimAsync(user3, new System.Security.Claims.Claim("role", "user"));
            }
        }
    }
}
