using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.Entities;
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

        static public IApplicationBuilder Seed(this IApplicationBuilder builder)
        {
          
            using ( var scope = builder.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RiodeDbContext>();
                db.Database.Migrate();
                InitBlogPosts(db);
            }
            return builder;
        }

        private static void InitBlogPosts(RiodeDbContext db)
        {
            if (!db.BlogPosts.Any())
            {
                string body= @"
                                        <p class='mb-5'> Lorem ipsum dolor sit amet, consectetuer adipiscing elit.
                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna. In nisi neque,
                                            aliquet vel, dapibus id, mattis vel, nisi. Sed pretium, ligula sollicitudin
                                            laoreet viverra, tortor libero sodales leo, eget blandit nunc tortor eu
                                            nibh. Nullam mollis. Ut justo. Suspendisse potenti.</p>

                                        <p class='mb-6'>Sed egestas, ante et vulputate volutpat, eros pede semper est,
                                            vitae luctus metus libero eu augue. Morbi purus libero, faucibus adipiscing,
                                            commodo quis, gravida id, est. Sed lectus. Praesent elementum hendrerit
                                            tortor. Sed semper lorem at felis. Vestibulum volutpat, lacus a <a href='#'>ultrices sagittis</a>, mi neque euismod dui, eu pulvinar nunc
                                            sapien ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et, dapibus
                                            sed, urna.</p>
                                        <div class='with-img row align-items-center'>
                                            <div class='col-md-6 mb-6'>
                                                <figure>
                                                    <img src='/Uploads/images/blog/single/2.jpg' alt='image' width='336' height='415' class='float-left'>
                                                    <figcaption class='text-left mt-1'>
                                                        Designe by <a href='#'>Casper Dalin</a>
                                                    </figcaption>
                                                </figure>
                                            </div>
                                            <div class='col-md-6 mb-6'>
                                                <h4 class='font-weight-semi-bold ls-s'>Quisque volutpat mattiseros.
                                                </h4>
                                                <p class='mb-8 col-lg-11'>Sed pretium, ligula sollicitudin laoreet
                                                    viverra, tortor libero sodales leo,
                                                    eget blandit nunc tortor eu nibh. Nullam mollis. Ut justo.
                                                    Suspendisse potenti. </p>
                                                <h4 class='font-weight-semi-bold ls-s'>More Details</h4>
                                                <ul class='list list-type-check mb-6'>
                                                    <li>Praesent id enim sit amet.</li>
                                                    <li>Tdio vulputate eleifend in in tortor. ellus massa.</li>
                                                    <li>Massa ristique sit amet condim vel</li>
                                                    <li>Dilisis Facilisis quis sapien. Praesent id enim sit amet</li>
                                                    <li>Praesent id enim sit amet.</li>
                                                    <li>Tdio vulputate eleifend in in tortor. ellus massa.</li>
                                                </ul>
                                            </div>
                                        </div>

                                        <blockquote class='mt-1 mb-6 p-relative'>
                                            <p class='font-weight-semi-bold ls-m'>
                                                “ Morbi purus libero, faucibus adipiscing,
                                                commodo quis, gravida id, est. Sed lectus.
                                                Praesent elementum hendrerit tortor. ”
                                            </p>
                                        </blockquote>

                                        <p>Morbi purus libero, faucibus adipiscing, commodo quis, gravida id, est. Sed
                                            lectus. Praesent elementum hendrerit tortor. Sed semper lorem at felis.
                                            Vestibulum volutpat, lacus a ultrices sagittis, mi neque euismod dui, eu
                                            pulvinar nunc sapien ornare nisl. Phasellus pede arcu, dapibus eu, fermentum
                                            et, dapibus sed, urna. Morbi interdum mollis sapien. Sed ac risus. Phasellus
                                            lacinia, magna a ullamcorper laoreet, lectus arcu pulvinar risus, vitae
                                            facilisis libero dolor a purus. </p>
";
                var category= db.Categories.FirstOrDefault();
                if (category == null)
                {
                    category = new Category
                    {
                        Name="Accesories"
                    };
                    db.Categories.Add(category);
                }
                db.BlogPosts.Add(new BlogPost
                {
                    Title = "Explore Fashion Trending For Women In Autumn 2020",
                    Body = body,
                    ImagePath="1_lg.jpg",
                    Category= category,
                    PublishedDate= DateTime.Now
                });
                db.BlogPosts.Add(new BlogPost
                {
                    Title = "Complete Set Of Ski Tools",
                    Body = body,
                    ImagePath = "3_lg.jpg",
                    Category = category,
                    PublishedDate = DateTime.Now
                });
                db.BlogPosts.Add(new BlogPost
                {
                    Title = "Explore Fashion Ipad And Accessories",
                    Body = body,
                    ImagePath = "4_lg.jpg",
                    Category = category,
                    PublishedDate = DateTime.Now
                });
                db.BlogPosts.Add(new BlogPost
                {
                    Title = "The Best Choice For Spending Time",
                    Body = body,
                    ImagePath = "5_lg.jpg",
                    Category = category,
                    PublishedDate = DateTime.Now
                });
                db.BlogPosts.Add(new BlogPost
                {
                    Title = "Women's Fashion Summer Dress",
                    Body = body,
                    ImagePath = "6_lg.jpg",
                    Category = category,
                    PublishedDate = DateTime.Now
                });
                db.SaveChangesAsync();
            }
        }
    }
}
