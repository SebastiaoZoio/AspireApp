﻿using AspireApp.Web.Components.Configurations;
using Microsoft.Extensions.Options;
using AspireApp.Web.Services.Models;

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

    public async Task<List<Collaborator>> GetCollaboratorsAsync()
    {
        var collaborators = await _httpClient.GetFromJsonAsync<List<Collaborator>>($"{_baseUri}collaborators");
        return collaborators;
    }        

    public async Task<HttpResponseMessage> AddCollaboratorAsync(Collaborator Collaborator)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}collaborator", Collaborator);
        return response;
    }

    public async Task<bool> RemoveCollaboratorAsync(int CollaboratorId)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUri}collaborators/{CollaboratorId}");
        return response.IsSuccessStatusCode;
    }
}