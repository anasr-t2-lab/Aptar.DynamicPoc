using Aptar.DynamicPoc.Data.Extensions;
using Aptar.DynamicPoc.Entities;
using Aptar.DynamicPoc.Services.SchemaDynamicValidation;
using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

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
              .HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Converters = { new ValidationRuleConverter() }
                }),
                v => JsonConvert.DeserializeObject<List<Field>>(v, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Converters = { new ValidationRuleConverter() }
                }))
            .HasColumnType("jsonb")
                  .IsRequired();
        }
    }
}
