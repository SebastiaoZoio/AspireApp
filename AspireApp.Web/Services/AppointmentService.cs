using AspireApp.Web.Components.Configurations;
using AspireApp.Web.Services.Models;
using AspireApp.Web.Services.Requests;
using Microsoft.Extensions.Options;

namespace AspireApp.Web.Services;

public class AppointmentService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUri;

    public AppointmentService(HttpClient httpClient, IOptions<ApiSettings> settings)
    {
        _httpClient = httpClient;
        _baseUri = settings.Value.BaseUri;
    }

    public async Task<IEnumerable<AppointmentType>> GetAppointmentTypes()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<AppointmentType>>($"{_baseUri}appointment-types");
    }

    public async Task<HttpResponseMessage> NewAppointmentAsync(NewAppointmentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}appointment", request);
        return response;
    }

    public async Task<HttpResponseMessage> ListAppointmentsAsync()
    {
//TODO
        var response = await _httpClient.GetAsync($"{_baseUri}appointments");
        return response;
    }
}
