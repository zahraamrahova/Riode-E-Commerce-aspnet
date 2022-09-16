using Microsoft.AspNetCore.Identity;
using Riode.WebUI.Models.Entities.Membership;

namespace Riode.WebUI.Models.DAL
{
    static public class RiodeDbSeed
    {
        static public IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            using (var scope= app.ApplicationServices.CreateScope())
            {
                var role = new RiodeRole
                {
                    Name = "SuperAdmin"
                };
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<RiodeUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RiodeRole>>();
                if (roleManager.RoleExistsAsync(role.Name).Result)
                {
                    role = roleManager.FindByNameAsync(role.Name).Result;
                }
                else 
                {
                    var roleCreateResult = roleManager.CreateAsync(role).Result;
                    if (!roleCreateResult.Succeeded)
                        goto end;
                }

                string pwd = "12345";
                var user = new RiodeUser
                {
                    UserName = "Zahra",
                    Email = "zehra.khudaverdiyeva@gmail.com",
                    EmailConfirmed = true,
                    Name="Zahra",
                    Surname="Amrahova"
                };
                var foundUser = userManager.FindByEmailAsync(user.Email).Result;
                if (foundUser != null && !userManager.IsInRoleAsync(foundUser, role.Name).Result)
                {
                    userManager.AddToRoleAsync(foundUser, role.Name).Wait();
                }
                else if (foundUser == null)
                {
                    var userCreateResult = userManager.CreateAsync(user, pwd).Result;
                    if (!userCreateResult.Succeeded)
                        goto end;
                    userManager.AddToRoleAsync(user, role.Name).Wait();

                }

            
            }
            end:
            return app;

        }
    }
}
