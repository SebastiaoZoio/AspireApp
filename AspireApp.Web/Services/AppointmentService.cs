using AspireApp.Web.Components.Configurations;
using AspireApp.Web.Services.Enums;
using AspireApp.Web.Services.Models;
using AspireApp.Web.Services.Requests;
using AspireApp.Web.Services.Results;
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

    public async Task<ListAppointmentTypesResult> GetAppointmentTypesAsync()
    {
        var result = new ListAppointmentTypesResult();

        try
        {
            var response = await _httpClient.GetAsync($"{_baseUri}appointment-types");

            if (response.IsSuccessStatusCode)
            {
                var appointmentTypes = await response.Content.ReadFromJsonAsync<IEnumerable<AppointmentType>>();
                result.AppointmentTypes = appointmentTypes;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = $"Failed to get appointment types. Status code: {response.StatusCode}";
                result.ErrorType = ErrorType.ServerError;
            }
        }
        catch (HttpRequestException ex)
        {
            result.Success = false;
            result.ErrorMessage = $"HttpRequestException: {ex.Message}";
            result.ErrorType = ErrorType.NetworkError;
        }
        catch (Exception ex)
        {
            result.Success = false;
        }

        return result;
    }

    public async Task<HttpResponseMessage> NewAppointmentAsync(NewAppointmentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUri}appointment", request);
        return response;
    }

    public async Task<ListAppointmentsResult> ListAppointmentsAsync()
    {
        var result = new ListAppointmentsResult();

        try
        {
            var response = await _httpClient.GetAsync($"{_baseUri}appointments");

            if (response.IsSuccessStatusCode)
            {
                var appointments = await response.Content.ReadFromJsonAsync<IEnumerable<Appointment>>();
                result.Appointments = appointments;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = $"Error retrieving appointments. Status Code: {response.StatusCode}";
                result.ErrorType = ErrorType.ServerError;
            }
        }
        catch (HttpRequestException ex)
        {
            result.Success = false;
            result.ErrorMessage = $"Request error: {ex.Message}";
            result.ErrorType = ErrorType.NetworkError;
        }
        catch (Exception ex)
        {
            result.Success = false;
        }

        return result;
    }
}
