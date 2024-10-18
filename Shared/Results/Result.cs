namespace CarShop.Domain.Results;

public enum ErrorType
{
    ValidationError,
    NotFound,
    InternalServerError,
    None
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public ErrorType ErrorType { get; }
    public T Value { get; }

    private Result(bool isSuccess, T value, string error, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
        ErrorType = errorType;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, null, ErrorType.None);

    public static Result<T> Failure(string error, ErrorType errorType) => new Result<T>(false, default, error, errorType);
}

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public ErrorType ErrorType { get; }

    private Result(bool isSuccess, string error, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorType = errorType;
    }

    public static Result Success() => new Result(true, null, ErrorType.None);

    public static Result Failure(string error, ErrorType errorType) => new Result(false, error, errorType);
}
