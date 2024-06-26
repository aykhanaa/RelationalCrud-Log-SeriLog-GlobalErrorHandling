using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuriations
{
    public class BaseConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
       

        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.Property(m => m.SoftDelete).HasDefaultValue(false);
            builder.Property(m => m.CreatedDate).HasDefaultValue(DateTime.Now);


        }
    }
}
