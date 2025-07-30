using Baiomy.FCI2.BLL.Common.AttatchmentService;
using Baiomy.FCI2.BLL.Services.Departments;
using Baiomy.FCI2.BLL.Services.Employees;
using Baiomy.FCI2.DAL.Entities.Employees;
using Baiomy.FCI2.DAL.Entities.Identity;
using Baiomy.FCI2.DAL.Persistence.Data;
using Baiomy.FCI2.DAL.Persistence.Repositories.Departments;
using Baiomy.FCI2.DAL.Persistence.Repositories.Employees;
using Baiomy.FCI2.DAL.Persistence.UnitOfWork;
using Baiomy.FCI2.PL.Controllers;
using Baiomy.FCI2.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Baiomy.FCI2.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            #region ConfigureService 

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BaiomyFCI2DbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IAttatchmentService, AttatchmentService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<ILogger<DepartmentController>, Logger<DepartmentController>>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<ILogger<EmployeeController>, Logger<EmployeeController>>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 5;
                option.Password.RequireNonAlphanumeric = true;

                option.User.RequireUniqueEmail = true;

                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);


            })
                .AddEntityFrameworkStores<BaiomyFCI2DbContext>()
                .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/SignIn";
                option.AccessDeniedPath = "/home/Error";

            });

            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure 
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run(); 
            #endregion
        }
    }
}
