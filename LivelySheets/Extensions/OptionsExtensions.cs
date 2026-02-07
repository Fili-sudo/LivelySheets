using LivelySheets.Options;

namespace LivelySheets.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        return services.Configure<AuthOptions>(
            configurationManager.GetSection(AuthOptions.Auth));
    }
}
