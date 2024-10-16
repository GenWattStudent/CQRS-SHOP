﻿namespace CarShop.Domain.Results;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public T Value { get; }

    private Result(bool isSuccess, T value, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, null);

    public static Result<T> Failure(string error) => new Result<T>(false, default, error);
}

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }

    private Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null);

    public static Result Failure(string error) => new Result(false, error);
}