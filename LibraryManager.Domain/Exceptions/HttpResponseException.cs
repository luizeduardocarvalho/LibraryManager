namespace LibraryManager.Domain.Exceptions;

public class HttpResponseException : Exception
{
    public HttpResponseException(int statusCode, string message) =>
        (StatusCode, Value) = (statusCode, message);

    public int StatusCode { get; }

    public string Value { get; }
}
