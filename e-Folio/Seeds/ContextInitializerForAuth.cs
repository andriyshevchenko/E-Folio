using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eFolio.EF;
using Microsoft.AspNetCore.Identity;

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
                    LastName = "Burko"
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
              var res =  await userManager.CreateAsync(user3, "Pass1234@");
                 
            }
        }
    }
}
