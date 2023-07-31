using Microsoft.AspNetCore.Authentication.Cookies;

namespace Rent_a_car_main_page
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Access/Login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=_mainPage}/{action=_mainPage}/{id?}");

            app.Run();
        }
    }
}