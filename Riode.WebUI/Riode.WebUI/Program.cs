using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Midlewares;
using Riode.WebUI.AppCode.Providers;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNewtonsoftJson(cfg =>
            {
                cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);
            builder.Services.AddDbContext<RiodeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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