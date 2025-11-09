using Microsoft.EntityFrameworkCore;
using FormsApp.Data;

namespace FormsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddControllersWithViews();

            /* var dbPath = Environment.GetEnvironmentVariable("FORMSAPP_DB") ??
                          Path.Combine(Directory.GetCurrentDirectory(), "formsapp.db"); */

            // var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            // var dbPath = Path.Combine(projectRoot, "formsapp.db");

            var dbPath = Environment.GetEnvironmentVariable("FORMSAPP_DB")
             ?? Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..")), "formsapp.db");

            Console.WriteLine("Using DB at: " + dbPath);


            Console.WriteLine("Using DB at: " + dbPath);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

            var app = builder.Build();

            app.UseSession();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Apply migrations
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
