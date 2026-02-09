using LivelySheets.CatalogService.API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LivelySheets.CatalogService.API.Endpoints.Authorize;

public class GenerateToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", (IOptions<AuthOptions> options) =>
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Results.Ok(jwtToken);
        })
        .WithName("GetToken")
        .Produces<string>()
        .WithOpenApi();
    }
}
