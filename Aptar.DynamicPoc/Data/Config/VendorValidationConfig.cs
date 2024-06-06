using Aptar.DynamicPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aptar.DynamicPoc.Data.Config
{
    public class VendorValidationConfig : IEntityTypeConfiguration<VendorValidation>
    {
        public void Configure(EntityTypeBuilder<VendorValidation> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FormSchema).HasColumnType("jsonb").IsRequired();
        }
    }
}
