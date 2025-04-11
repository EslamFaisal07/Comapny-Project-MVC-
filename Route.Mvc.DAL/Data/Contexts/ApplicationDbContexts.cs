using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Route.Mvc.DAL.Models.DepartmentModel;
using Route.Mvc.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Data.Contexts
{
    public class ApplicationDbContexts(DbContextOptions<ApplicationDbContexts> options) : IdentityDbContext<ApplicationUser>(options)
    {


        //override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=RouteMvcDb;Trusted_Connection=True;TrustServerCertificate = true");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }




        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }







    }
}
