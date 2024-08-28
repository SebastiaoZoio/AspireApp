using AspireApp.Web.Components.Configurations;
using Microsoft.Extensions.Options;
using AspireApp.Web.Services.Models;
using AspireApp.Web.Services.Requests;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using AspireApp.Web.Services.Responses;
using AspireApp.Web.Services.Results;
using System.Net;
using AspireApp.Web.Services.Exceptions;
using AspireApp.Web.Services.Enums;

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

    public async Task<ListCollaboratorsResult> ListCollaboratorsAsync(ListCollaboratorsRequest request)
    {
        return await SendCollaboratorsRequestAsync($"{_baseUri}list-collaborators", request);
    }

    public async Task<ListCollaboratorsResult> ListCollaboratorsAsync(GetCollaboratorsRequest request)
    {
        return await SendCollaboratorsRequestAsync($"{_baseUri}filtered-collaborators", request);
    }

    private async Task<ListCollaboratorsResult> SendCollaboratorsRequestAsync<TRequest>(string url, TRequest request)
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
            throw new WebApiException("Request timed out.", ex, ErrorType.Timeout);            
        }
        catch (WebApiException ex)
        {
            return new ListCollaboratorsResult()
            {
                Success = false,
                ErrorMessage = ex.Message,
                ErrorType = ex.ErrorType
            };
        }
        catch (Exception)
        {
            return new ListCollaboratorsResult() { Success = false};
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


    private async Task<ListCollaboratorsResult> ParseCollaboratorsResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new WebApiException($"Request failed with status code {response.StatusCode}", ErrorType.FailedRequest);
        }

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new WebApiException("Received an empty response from the server.", ErrorType.EmptyResponse);
        }

        JObject jsonResponse;
        try
        {
            jsonResponse = JObject.Parse(content);
        }
        catch (JsonException ex)
        {
            throw new WebApiException("Failed to parse JSON response.", ex, ErrorType.InvalidResponse);
        }

        if (jsonResponse["collaborators"] == null || jsonResponse["totalCount"] == null)
        {
            throw new WebApiException("Response JSON does not contain the expected fields.", ErrorType.InvalidResponse);
        }

        var result = new ListCollaboratorsResult()
        {
            Collaborators = jsonResponse["collaborators"].ToObject<IEnumerable<Collaborator>>(),
            TotalCount = jsonResponse["totalCount"].ToObject<int>()
        };
        var collaborators = jsonResponse["collaborators"].ToObject<IEnumerable<Collaborator>>();
        var totalCount = jsonResponse["totalCount"].ToObject<int>();

        return result;
    }
}