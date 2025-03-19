global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Route.Mvc.DAL.Models;

 
namespace Route.Mvc.DAL.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10,10);

            builder.Property(d=>d.Name).HasColumnType("varchar(20)");
            builder.Property(d=>d.Code).HasColumnType("varchar(20)");

            builder.Property(d=>d.CreatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(d=>d.LastModifiedOn).HasComputedColumnSql("GETDATE()");




        }
    }
}
