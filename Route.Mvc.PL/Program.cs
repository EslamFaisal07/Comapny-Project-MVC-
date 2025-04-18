using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Mvc.BusinessLL.Profiles;
using Route.Mvc.BusinessLL.Services.Classes;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.DAL.Data.Contexts;
using Route.Mvc.DAL.Models;
using Route.Mvc.DAL.Repositories.Classes;
using Route.Mvc.DAL.Repositories.Interfaces;
using Route.Mvc.PL.Helpers;
using Route.Mvc.PL.Settings;

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
                options.UseLazyLoadingProxies();
            });


            //builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService , DepartmentService>();
            builder.Services.AddScoped<IAttachmentService,AttachmentServices >();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
             .AddEntityFrameworkStores<ApplicationDbContexts>()
             .AddDefaultTokenProviders();


            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IMailService, MailService>();


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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            #endregion



            app.Run();
        }
    }
}
