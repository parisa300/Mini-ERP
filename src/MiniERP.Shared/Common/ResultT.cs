namespace MiniERP.Shared.Common;

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value)
        : base(true, null)
    {
        Value = value;
    }

    private Result(string error)
        : base(false, error)
    {
    }

    public static Result<T> Success(T value)
        => new(value);

    public static new Result<T> Failure(string error)
        => new(error);
}