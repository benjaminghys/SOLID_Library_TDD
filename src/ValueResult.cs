namespace LibraryExercise;

/// <summary>
///     Represents the result of an operation, with a status indicating success or failure,
///     an optional value, and an optional error message.
/// </summary>
/// <typeparam name="T">The type of the value to be stored in the result.</typeparam>
public class Result<T> : IResult<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Result{T}" /> class.
    /// </summary>
    /// <param name="isSuccess">Whether the result was successful.</param>
    /// <param name="value">The value to store.</param>
    /// <param name="errorMessage">The message to store when the result failed.</param>
    private Result(bool isSuccess, T value, string errorMessage = "")
    {
        Success = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    /// <inheritdoc cref="IResult.Success" />
    public bool Success { get; }

    /// <inheritdoc cref="IResult{T}.Value" />
    public T Value { get; }

    /// <inheritdoc cref="IResult{T}.ErrorMessage" />
    public string ErrorMessage { get; }

    /// <summary>
    ///     Creates a successful result with a specified value.
    /// </summary>
    /// <param name="value">The value to return.</param>
    /// <returns>A successful result with a value.</returns>
    public static Result<T> SuccessResult(T value)
    {
        return new Result<T>(true, value);
    }

    /// <summary>
    ///     Creates a failed result with an error message.
    /// </summary>
    /// <param name="errorMessage">The error message to pass on.</param>
    /// <returns>A failed result with an error message.</returns>
    public static Result<T?> FailResult(string errorMessage)
    {
        return new Result<T?>(false, default, errorMessage);
    }

    /// <summary>
    ///     Implicit conversion to boolean for simplifying result checking in conditions.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    public static implicit operator bool(Result<T> result)
    {
        return result.Success;
    }

    /// <summary>
    ///     Provides a string representation of the result.
    /// </summary>
    /// <returns>String representation of the result.</returns>
    public override string ToString()
    {
        return Success ? $"Success: {Value}" : $"Failure: {ErrorMessage}";
    }
}