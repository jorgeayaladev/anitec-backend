namespace Titan.AniTec.Platform.Shared.Application.Model;

public class Result<T>
{
    protected Result(bool isSuccess, T? value, string message, Enum? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Message = message;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string Data => Value?.ToString() ?? string.Empty;
    public string Message { get; }
    public Enum? Error { get; }

    public static Result<T> Success(T value)
        => new(true, value, string.Empty, null);

    public static Result<T> Failure(Enum error)
        => new(false, default, string.Empty, error);

    public static Result<T> Failure(Enum error, string message)
        => new(false, default, message, error);

    public Result<TDest> Map<TDest>(Func<T, TDest> mapper)
        => IsSuccess
            ? Result<TDest>.Success(mapper(Value!))
            : Result<TDest>.Failure(Error!, Message);
}

public class Result
{
    private protected Result(bool isSuccess, string message, Enum? error)
    {
        IsSuccess = isSuccess;
        Message = message;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; }
    public Enum? Error { get; }

    public static Result Success()
        => new(true, string.Empty, null);

    public static Result Failure(Enum error)
        => new(false, string.Empty, error);

    public static Result Failure(Enum error, string message)
        => new(false, message, error);
}
