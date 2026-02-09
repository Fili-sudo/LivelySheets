using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LivelySheets.CatalogService.API.Extensions;

public static class AuthExtensions
{
    public static readonly string Auth = nameof(Auth);
    public static IServiceCollection ConfigureJWTAuthentication(this IServiceCollection services, IConfigurationSection configurationSection)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configurationSection.GetValue<string>("Issuer"),
                ValidAudience = configurationSection.GetValue<string>("Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationSection.GetValue<string>("SigningKey")!))
            };
        });
        services.AddAuthorization();

        return services;
    }
}
