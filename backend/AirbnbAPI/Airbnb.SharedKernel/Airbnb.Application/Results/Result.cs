namespace Airbnb.Application.Results;

public record Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public List<string> Errors { get; } = [];

    private Result(T value) { IsSuccess = true; Value = value; }
    private Result(List<string> errors) { IsSuccess = false; Errors = errors; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(List<string> errors) => new(errors);
}