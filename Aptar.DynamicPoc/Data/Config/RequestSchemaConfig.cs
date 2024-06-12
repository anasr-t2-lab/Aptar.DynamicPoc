using Aptar.DynamicPoc.Data.Extensions;
using Aptar.DynamicPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aptar.DynamicPoc.Data.Config
{
    public class RequestSchemaConfig : IEntityTypeConfiguration<RequestSchema>
    {
        public void Configure(EntityTypeBuilder<RequestSchema> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Fields)
               .HasListJsonConversion()
                  .IsRequired();
        }
    }
}
