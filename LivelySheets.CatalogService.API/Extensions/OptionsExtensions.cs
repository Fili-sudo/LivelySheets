using LivelySheets.CatalogService.API.Options;

namespace LivelySheets.CatalogService.API.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        return services.Configure<AuthOptions>(
            configurationManager.GetSection(AuthOptions.Auth));
    }
}
