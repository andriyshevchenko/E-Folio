using eFolio.EF;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eFolio.API.Seeds
{
    public class ContextInitializerForAuth
    {
        public static async Task Initialize(AuthDBContext context, UserManager<UserEntity> userManager)
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

                await userManager.CreateAsync(user3, "Pass1234@");

                foreach (var userEntity in new UserEntity[] { user1, user2, user3 })
                {
                    await userManager.AddClaimAsync(userEntity, new Claim("role", "user"));
                }
            }
        }
    }
}
