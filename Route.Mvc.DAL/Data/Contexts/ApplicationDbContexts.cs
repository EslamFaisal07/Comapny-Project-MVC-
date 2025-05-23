﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Data.Contexts
{
    public class ApplicationDbContexts(DbContextOptions<ApplicationDbContexts> options) : DbContext(options)
    {


        //override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=RouteMvcDb;Trusted_Connection=True;TrustServerCertificate = true");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }




        public DbSet<Department> Departments { get; set; }







    }
}
