using Route.Mvc.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");



        }
    }
}
