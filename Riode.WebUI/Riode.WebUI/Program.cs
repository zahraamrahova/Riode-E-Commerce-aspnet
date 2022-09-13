using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.BrandModule;
using Riode.WebUI.AppCode.Application.SpecificationModule;
using Riode.WebUI.AppCode.Midlewares;
using Riode.WebUI.AppCode.Providers;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities.Membership;
using System.Reflection;

namespace Riode.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(x =>
            {
                var policy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();
                x.Filters.Add(new AuthorizeFilter(policy));
            }).AddRazorRuntimeCompilation().AddNewtonsoftJson(cfg =>
            {
                cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;              
            });
            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);
            builder.Services.AddDbContext<RiodeDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))).AddIdentity<RiodeUser, RiodeRole>().AddEntityFrameworkStores<RiodeDbContext>();
            builder.Services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                //cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequiredLength = 3;
                cfg.User.RequireUniqueEmail = true;
                //cfg.User.AllowedUserNameCharacters = "abcd....";
                cfg.Lockout.MaxFailedAccessAttempts = 3;
                cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 3, 0);

            });
            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";
                cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
                cfg.Cookie.Name = "Riode";
            });
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddScoped <UserManager<RiodeUser>> ();
            builder.Services.AddScoped<SignInManager<RiodeUser>>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            //builder.Services.AddMediatR(typeof(BrandSingleQuery).GetTypeInfo().Assembly);
            //builder.Services.AddMediatR(typeof(BrandCreateCommand).GetTypeInfo().Assembly);
            //builder.Services.AddMediatR(typeof(BrandEditCommand).GetTypeInfo().Assembly);
            //builder.Services.AddMediatR(typeof(BrandRemoveCommand).GetTypeInfo().Assembly);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedUICultures("az", "en");
                cfg.AddSupportedCultures("az", "en");
                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });

            app.UseAudit();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints => {

                endpoints.MapGet("/coming-soon.html", async (context) =>
                {
                    using (var sr = new StreamReader("views/static/coming-soon.html"))
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync(sr.ReadToEnd());
                    }
                });
                    endpoints.MapControllerRoute(
                    name: "areas-with-lang",
                    pattern: "{lang}/{area:exists}/{controller=Dashboard}/{action=Index}/{id?}",
                    constraints: new
                    {
                        lang= "en|az|ru"
                    }
                 );
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default-signin",
                    pattern: "signin.html",
                    defaults: new
                    {
                        area = "",
                        controller = "account",
                        action= "signin"
                    });
                endpoints.MapControllerRoute(
                    name: "default-register",
                    pattern: "register.html",
                    defaults: new
                    {
                        area = "",
                        controller = "account",
                        action = "register"
                    });
                endpoints.MapControllerRoute(
                  name: "default-accessdenied",
                  pattern: "accessdenied.html",
                  defaults: new
                  {
                      area = "",
                      controller = "account",
                      action = "accessdenied"
                  });

                endpoints.MapControllerRoute("default-with-lang", "{lang}/{controller=Home}/{action=Index}/{id?}", constraints: new
                {
                    lang = "en|az|ru"
                });
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });


            app.Run();
        }
    }
}