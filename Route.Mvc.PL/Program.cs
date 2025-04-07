using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Mvc.BusinessLL.Profiles;
using Route.Mvc.BusinessLL.Services.Classes;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.DAL.Data.Contexts;
using Route.Mvc.DAL.Repositories.Classes;
using Route.Mvc.DAL.Repositories.Interfaces;

namespace Route.Mvc.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region  Add services to the container

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddDbContext<ApplicationDbContexts>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService , DepartmentService>();


            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));




            #endregion



            var app = builder.Build();



            #region Configure the HTTP request pipeline.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion



            app.Run();
        }
    }
}
