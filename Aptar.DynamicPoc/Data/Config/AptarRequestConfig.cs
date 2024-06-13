using Aptar.DynamicPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Data.Config
{
    public class AptarRequestConfig : IEntityTypeConfiguration<AptarRequest>
    {

        public void Configure(EntityTypeBuilder<AptarRequest> builder)
        {

            var jsonObjectConverter = new ValueConverter<JObject, string>(
             v => v.ToString(Formatting.None),
             v => JObject.Parse(v));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Body).HasConversion(jsonObjectConverter).HasColumnType("jsonb").IsRequired();

            builder.HasOne<RequestSchema>()
                .WithMany()
                .HasForeignKey(x => x.RequestTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
