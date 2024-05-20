using Microsoft.EntityFrameworkCore;
using SchoolManager.Database;
using SchoolManager.Database.Services;
using SchoolManager.Resources.Interface;

namespace SchoolManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<SchoolDbContext>(options =>
               options.UseSqlite(builder.Configuration.GetConnectionString("SchoolDbConnection")));

            builder.Services.AddScoped<ISchoolService<CourseService, GroupService, StudentService>, SchoolService>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Додавання контексту бази даних
                services.AddDbContext<SchoolDbContext>(options =>
                    options.UseSqlite(hostContext.Configuration.GetConnectionString("SchoolDbConnection")));

                // Додавання інших служб
                // services.AddOtherServices();
            });
    }
}
