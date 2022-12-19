using Microsoft.OpenApi.Models;

namespace API.Errors;

public class ApiException : ApiResponse
{
    public string Exception { get; set; }

    public ApiException(int statusCode): base(statusCode)
    {
        
    }
    
    public ApiException(int statusCode, string message, string exception) : base(statusCode,message)
    {
        Exception = exception;
    }
}