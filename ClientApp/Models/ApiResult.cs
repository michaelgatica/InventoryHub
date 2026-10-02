namespace ClientApp.Models;

public class ApiResult<T>
{
    private ApiResult(bool isSuccess, T? value, string? message)
    {
        IsSuccess = isSuccess;
        Value = value;
        Message = message;
    }

    public bool IsSuccess { get; }

    public T? Value { get; }

    public string? Message { get; }

    public static ApiResult<T> Success(T value, string? message = null)
    {
        return new ApiResult<T>(true, value, message);
    }

    public static ApiResult<T> Failure(string message)
    {
        return new ApiResult<T>(false, default, message);
    }
}
