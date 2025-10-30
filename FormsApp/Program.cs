using Microsoft.EntityFrameworkCore;
using FormsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FormsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "formsapp.db");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(@"Data Source=C:\Users\pimid\source\repos\forms-app\FormsApp\formsapp.db"));

            builder.Services.AddDistributedMemoryCache(); // required for session storage
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
            });

            Console.WriteLine("DB Path: " + Path.GetFullPath(@"C:\Users\pimid\source\repos\forms-app\FormsApp\formsapp.db"));

            var app = builder.Build();

            app.UseSession(); // enables session middleware

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
