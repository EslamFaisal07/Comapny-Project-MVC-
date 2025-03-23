using Route.Mvc.DAL.Models.DepartmentModel;
using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Data.Configurations
{
    public class EmployeeConfigurations : BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e=>e.Name).HasColumnType("varchar(50)");
            builder.Property(e=>e.Address).HasColumnType("varchar(150)");
            builder.Property(e=>e.Salary).HasColumnType("decimal(10,2)");


            builder.Property(e=>e.Gender).HasConversion((gender)=>gender.ToString(),
                EmpGender=> (Gender)Enum.Parse(typeof(Gender) , EmpGender));

            builder.Property(e => e.EmployeeType).HasConversion(type=>type.ToString(),
                empType=>(EmployeeType)Enum.Parse(typeof(EmployeeType) , empType));

            base.Configure(builder);


        }





    }
}
