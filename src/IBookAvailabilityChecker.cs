namespace LibraryExercise
{
    /// <summary>
    /// The Book Availability Checker interface.
    /// </summary>
    public interface IBookAvailabilityChecker
    {
        /// <summary>
        /// Checks if a book is available.
        /// </summary>
        /// <param name="isbn">ISBN book code to check.</param>
        /// <returns><c>true</c> when the book is available, <c>false</c> otherwise.</returns>
        IResult<bool> IsAvailable(ISBN isbn);
    }
}