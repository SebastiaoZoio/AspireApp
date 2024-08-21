using AspireApp.Web.Components.Configurations;
using Microsoft.Extensions.Options;
using AspireApp.Web.Services.Models;
using System.Net.Http.Json;
using AspireApp.Web.Services.Requests;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text;
using AspireApp.Web.Services.Responses;
using AspireApp.Web.Services.Results;
using System.Net;

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

    public async Task<(IEnumerable<Collaborator>, int)> ListCollaboratorsAsync(ListCollaboratorsRequest request)
    {
        return await SendCollaboratorsRequestAsync($"{_baseUri}list-collaborators", request);
    }

    public async Task<(IEnumerable<Collaborator>, int)> ListCollaboratorsAsync(GetCollaboratorsRequest request)
    {
        return await SendCollaboratorsRequestAsync($"{_baseUri}filtered-collaborators", request);
    }

    private async Task<(IEnumerable<Collaborator>, int)> SendCollaboratorsRequestAsync<TRequest>(string url, TRequest request)
    {
        try
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30))) // Setting a timeout of 30 seconds
            {
                var response = await _httpClient.PostAsJsonAsync(url, request, cts.Token);
                return await ParseCollaboratorsResponse(response);
            }
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            throw new HttpRequestException("Request timed out.", ex);
        }
        catch (HttpRequestException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> AddCollaboratorAsync(NewCollaboratorRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}collaborator", request);
        return response;
    }

    public async Task<RemoveCollaboratorsResult> RemoveCollaboratorsAsync(DeleteCollaboratorsRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}delete-collaborators", request);

        if (response.IsSuccessStatusCode)
        {
            return new RemoveCollaboratorsResult
            {
                Success = true,
                Message = "Collaborators removed successfully."
            };
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            var errorContent = await response.Content.ReadFromJsonAsync<CollaboratorsNotFoundResponse>();
            return new RemoveCollaboratorsResult
            {
                Success = false,
                Message = errorContent?.Message,
                NotFoundCollaborators = errorContent?.Names ?? new List<string>()
            };
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new RemoveCollaboratorsResult
            {
                Success = false,
                Message = $"Unexpected error: {response.StatusCode}. Details: {errorContent}"
            };
        }
    }


    private async Task<(IEnumerable<Collaborator>, int)> ParseCollaboratorsResponse(HttpResponseMessage response)
    {
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