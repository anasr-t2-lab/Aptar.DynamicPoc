using Aptar.DynamicPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Data.Config
{
    public class RequestTypeConfig : IEntityTypeConfiguration<RequestType>
    {
        public void Configure(EntityTypeBuilder<RequestType> builder)
        {
            var jsonArrayConverter = new ValueConverter<JArray, string>(
             v => v.ToString(Formatting.None),
             v => JArray.Parse(v));

            builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FormSchema)
               .HasConversion(jsonArrayConverter)
                  .HasColumnType("jsonb")
                  .IsRequired();
        }
    }
}
