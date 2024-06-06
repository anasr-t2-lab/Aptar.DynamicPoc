using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Aptar.DynamicPoc.Data;

public class DynamicPocEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public DynamicPocEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the DynamicPocDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DynamicPocDbContext>()
            .Database
            .MigrateAsync();
    }
}
