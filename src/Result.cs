namespace LibraryExercise
{
    /// <summary>
    /// Represents the result of an operation, with a status indicating success or failure,
    /// with an optional error message.
    /// </summary>
    public class Result : IResult
    {
        /// <inheritdoc cref="IResult.Success"/>
        public bool Success { get; }

        /// <inheritdoc cref="IResult.ErrorMessage"/>
        public string ErrorMessage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">Whether the result was successful.</param>
        /// <param name="errorMessage">The message to store when the result failed.</param>
        private Result(bool isSuccess, string errorMessage = "")
        {
            this.Success = isSuccess;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful result with a specified value.
        /// </summary>
        /// <returns>A successful result with a value.</returns>
        public static Result SuccessResult() => new Result(true);

        /// <summary>
        /// Creates a failed result with an error message.
        /// </summary>
        /// <param name="errorMessage">The error message to pass on.</param>
        /// <returns>A failed result with an error message.</returns>
        public static Result FailResult(string errorMessage) => new Result(false, errorMessage);

        /// <summary>
        /// Implicit conversion to boolean for simplifying result checking in conditions.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        public static implicit operator bool(Result result) => result.Success;

        /// <summary>
        /// Provides a string representation of the result.
        /// </summary>
        /// <returns>String representation of the result.</returns>
        public override string ToString() => this.Success ? "Success" : $"Failure: {this.ErrorMessage}";
    }
}