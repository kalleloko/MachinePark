namespace MachinePark.Client.Extensions;

using System.Text;
using System.Text.Json;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PatchReplaceAsync<T>(
        this HttpClient http,
        string url,
        string path,
        T value)
    {
        var operations = new[]
        {
            new
            {
                op = "replace",
                path = path.StartsWith("/") ? path : $"/{path}",
                value
            }
        };
        var json = JsonSerializer.Serialize(operations);

        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json-patch+json")
        };

        var response = await http.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return response;

    }
}