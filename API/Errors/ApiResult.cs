namespace API.Errors;

public class ApiResult
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public ApiResult(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }
    
    private string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "You have made a bad request",
            401 => "Authorized error",
            404 => "Resource has not been found",
            500 => "Internal Server Error...",
            _ => null
        };
    }
}