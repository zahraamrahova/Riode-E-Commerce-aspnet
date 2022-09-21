using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode;
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

        static internal string[] principals = null;
        public static void Main(string[] args)
        {

            var types=  typeof(Program).Assembly.GetTypes();
            principals = types.Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
                   .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                   .Union(types
                   .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                   .SelectMany(type => type.GetMethods())
                   .Where(method => method.IsPublic
                       && !method.IsDefined(typeof(NonActionAttribute))
                       && method.IsDefined(typeof(AuthorizeAttribute)))
                   .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>()))
                   .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                   .SelectMany(a => a.Policy.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                   .Distinct()
                   .ToArray();
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
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))).AddIdentity<RiodeUser, RiodeRole>()
            .AddEntityFrameworkStores<RiodeDbContext>()
            .AddDefaultTokenProviders();
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
            builder.Services.AddAuthorization(cfg =>

            {

                foreach (var policyName in principals)
                {
                    cfg.AddPolicy(policyName, p =>
                    {
                        p.RequireAssertion(handler =>
                        {
                            return handler.User.IsInRole("SuperAdmin") 
                            || handler.User.HasClaim(policyName, "1");
                        });
                    });
                }              
            });
            builder.Services.AddScoped <UserManager<RiodeUser>> ();
            builder.Services.AddScoped<SignInManager<RiodeUser>>();
            builder.Services.AddScoped<RoleManager<RiodeRole>>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.Seed();
            }
            app.SeedMembership();
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
                            
           
                //endpoints.MapControllerRoute(
                //  name: "default-accessdenied",
                //  pattern: "accessdenied.html",
                //  defaults: new
                //  {
                //      area = "",
                //      controller = "account",
                //      action = "accessdenied"
                //  });

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