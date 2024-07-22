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
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}list-collaborators", request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync();

        var jsonResponse = JObject.Parse(content);

        var collaborators = jsonResponse["collaborators"].ToObject<IEnumerable<Collaborator>>();
        var totalCount = jsonResponse["totalCount"].ToObject<int>();

        return (collaborators, totalCount);
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