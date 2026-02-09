namespace LivelySheets.CatalogService.API.Options;

public class AuthOptions
{
    public const string Auth = nameof(Auth);
    public string SigningKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
