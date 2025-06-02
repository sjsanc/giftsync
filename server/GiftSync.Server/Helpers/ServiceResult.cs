namespace GiftSync.Server.Helpers;

public class ServiceResult
{
    public bool Succeeded { get; protected set; }
    public string? ErrorMessage { get; protected set; }
    public Exception? Exception { get; protected set; }

    public static ServiceResult Success() => new ServiceResult { Succeeded = true };
    public static ServiceResult Failure(string errorMessage) => new ServiceResult { Succeeded = false, ErrorMessage = errorMessage };
    public static ServiceResult Failure(string errorMessage, Exception exception) => new ServiceResult { Succeeded = false, ErrorMessage = errorMessage, Exception = exception };
}

public class ServiceResult<T> : ServiceResult
{
    public T? Value { get; protected set; }

    public static ServiceResult<T> Success(T value) => new ServiceResult<T> { Succeeded = true, Value = value };
    public new static ServiceResult<T> Failure(string errorMessage) => new ServiceResult<T> { Succeeded = false, ErrorMessage = errorMessage };
    public new static ServiceResult<T> Failure(string errorMessage, Exception exception) => new ServiceResult<T> { Succeeded = false, ErrorMessage = errorMessage, Exception = exception };
}