namespace LibraryExercise
{
    /// <summary>
    /// Interface for a simple result.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets a value indicating whether the result was successful.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the error message to return when the result is not successful.
        /// </summary>
        string ErrorMessage { get; }
    }

    /// <summary>
    /// Interface for a result with a return value.
    /// </summary>
    /// <typeparam name="T">The type of value to be stored in the result.</typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// Gets the value to return.
        /// </summary>
        T Value { get; }
    }
}