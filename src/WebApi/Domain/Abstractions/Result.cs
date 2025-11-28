namespace WebApi.Domain.Abstractions;

public class Result<TValue>
{
    private readonly TValue? _value;
    private readonly bool _isSuccess;
    private readonly Error _error;

    private Result(TValue value)
    {
        _isSuccess = true;
        _value = value;
        _error = Error.None;
    }

    private Result(Error error)
    {
        _isSuccess = false;
        _value = default;
        _error = error;
    }

    public bool IsSuccess => _isSuccess;

    public bool IsFailure => !_isSuccess;

    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Failure(Error error) => new(error);

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> success, Func<Error, TResult> failure)
    {
        return _isSuccess ? success(_value!) : failure(_error);
    }

    public TValue GetValue() => Match(_ => _, e => throw new Exception("Expected success result"));

    public Error GetError() => Match(_ => throw new Exception("Expected error result"), e => e);
}
