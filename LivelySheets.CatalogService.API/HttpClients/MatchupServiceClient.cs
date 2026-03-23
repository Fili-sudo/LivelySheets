using System.Text;
using System.Text.Json;

namespace LivelySheets.CatalogService.API.HttpClients;

public class MatchupServiceClient(HttpClient httpClient)
{
    private readonly string CreateInboxMessageEndpoint = "messages/create-message";

    //TODO: Finish service method
    public async Task<string> SendOutboxMessageAsync()
    {
        var employee = JsonSerializer.Serialize(new
        {
            EmpId = Guid.NewGuid(),
            Name = "Jaimin",
            Address = "F302, Nakshtra Heights, Vadodara"
        });
        var request = new StringContent(employee, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(CreateInboxMessageEndpoint, request);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
        }

        return response.StatusCode.ToString();
    }
}
