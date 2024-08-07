using AspireApp.Web.Components.Configurations;
using Microsoft.Extensions.Options;
using AspireApp.Web.Services.Models;
using System.Net.Http.Json;
using AspireApp.Web.Services.Requests;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text;

namespace AspireApp.Web.Services;

public class CollaboratorService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUri;

    public CollaboratorService(HttpClient httpClient, IOptions<ApiSettings> settings)
    {
        _httpClient = httpClient;
        _baseUri = settings.Value.BaseUri;
    }

    public async Task<IEnumerable<Collaborator>> GetCollaboratorsAsync()
    {
        var collaborators = await _httpClient.GetFromJsonAsync<IEnumerable<Collaborator>>($"{_baseUri}collaborators");
        return collaborators;
    }

    public async Task<(IEnumerable<Collaborator>, int)> ListCollaboratorsAsync(ListCollaboratorsRequest request)
    {
        try
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30))) // Setting a timeout of 30 seconds
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUri}list-collaborators", request, cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new HttpRequestException("Received an empty response from the server.");
                }

                JObject jsonResponse;
                try
                {
                    jsonResponse = JObject.Parse(content);
                }
                catch (JsonException ex)
                {
                    throw new HttpRequestException("Failed to parse JSON response.", ex);
                }

                if (jsonResponse["collaborators"] == null || jsonResponse["totalCount"] == null)
                {
                    throw new HttpRequestException("Response JSON does not contain the expected fields.");
                }

                var collaborators = jsonResponse["collaborators"].ToObject<IEnumerable<Collaborator>>();
                var totalCount = jsonResponse["totalCount"].ToObject<int>();

                return (collaborators, totalCount);
            }
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            throw new HttpRequestException("Request timed out.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public async Task<HttpResponseMessage> AddCollaboratorAsync(Collaborator Collaborator)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}collaborator", Collaborator);
        return response;
    }

    public async Task<HttpResponseMessage> RemoveCollaboratorAsync(Guid CollaboratorId)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUri}collaborators/{CollaboratorId}");
        return response;
    }
}