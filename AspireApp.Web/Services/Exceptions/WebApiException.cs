using AspireApp.Web.Services.Enums;

namespace AspireApp.Web.Services.Exceptions;

public class WebApiException : HttpRequestException
{
    public ErrorType ErrorType;

    public WebApiException(string message, ErrorType errorType) : base(message)
    {
        this.ErrorType = errorType;
    }

    public WebApiException(string message, Exception innerException, ErrorType errorType) : base(message, innerException)
    {
        this.ErrorType = errorType;
    }
}
