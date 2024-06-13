using Aptar.DynamicPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text.Json;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Aptar.DynamicPoc.Data;

public class DynamicPocDbContext : AbpDbContext<DynamicPocDbContext>
{
    public DbSet<RequestType> RequestTypes { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<RequestSchema> RequestSchemas { get; set; }
    public DbSet<AptarRequest> AptarRequests { get; set; }


    public DynamicPocDbContext(DbContextOptions<DynamicPocDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        /* Include modules to your migration db context */
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own entities here */

        SeedValidations(builder);
    }


    public static void SeedValidations(ModelBuilder modelBuilder)
    {

        string formlyTemplate = @"
[
  {
    ""key"": ""subject"",
    ""type"": ""input"",
    ""props"": {
      ""label"": ""Subject"",
      ""required"": true,
      ""maxLength"": 50
    }
  },
  {
    ""key"": ""description"",
    ""type"": ""textarea"",
    ""props"": {
      ""label"": ""Description"",
      ""required"": true,
      ""maxLength"": 500
    }
  },
  {
    ""key"": ""samplesCount"",
    ""type"": ""input"",
    ""props"": {
      ""label"": ""Samples Count"",
      ""type"": ""number"",
      ""required"": true,
      ""min"": 1,
      ""max"": 10
    }
  },
  {
    ""key"": ""material"",
    ""type"": ""select"",
    ""props"": {
      ""label"": ""Material"",
      ""required"": true,
      ""options"": [
        { ""label"": ""Plastic"", ""value"": 1 },
        { ""label"": ""Aluminum"", ""value"": 2 },
        { ""label"": ""Glass"", ""value"": 3 }
      ]
    }
  },
  {
    ""key"": ""email"",
    ""type"": ""input"",
    ""props"": {
      ""label"": ""Email"",
      ""required"": true,
      ""type"": ""email""
    }
  },
  {
    ""key"": ""phoneNumber"",
    ""type"": ""input"",
    ""props"": {
      ""label"": ""Phone Number"",
      ""required"": false,
      ""pattern"": ""^[0-9]*$""
    }
  },
          {
            ""key"": ""requestDate"",
            ""type"": ""date"",
            ""props"": {
              ""label"": ""Request Date"",
              ""required"": true,
              ""minDate"": ""2024-01-01"",
              ""maxDate"": ""2024-12-31""
            }
    }
]
";

        var validations = new RequestType[]
        {
                new RequestType{ Id=1, Name = "Hamada", FormSchema = JArray.Parse(formlyTemplate)}
        };
        modelBuilder.Entity<RequestType>().HasData(validations);
    }
}
