namespace LibraryExercise
{
    /// <summary>
    /// Service for borrowing books.
    /// </summary>
    public interface IBookBorrowingService
    {
        /// <summary>
        /// Gets the total amount of books borrowed.
        /// </summary>
        public int TotalBorrowed { get; }

        /// <summary>
        /// Borrow a book from the library.
        /// </summary>
        /// <param name="isbn">The book <see cref="ISBN"/> to borrow.</param>
        /// <returns>
        ///     A success result if the book was borrowed successfully.
        ///     A failure result if the book is already borrowed or does not exist in the library.
        /// </returns>
        public IResult BorrowBook(ISBN isbn);

        /// <summary>
        /// Return a book to the library, making it available again for borrowing.
        /// </summary>
        /// <param name="isbn">The book <see cref="ISBN"/> to be returned.</param>
        /// <returns>
        ///     A success result if the book was returned successfully.
        ///     A failure result if the book does not exist in the library or was not borrowed.
        /// </returns>
        public IResult ReturnBook(ISBN isbn);

        /// <summary>
        /// Checks if a book is available.
        /// </summary>
        /// <param name="isbn">The book <see cref="ISBN"/> to check.</param>
        /// <returns><c>true</c> when the book is available, <c>false</c> otherwise.</returns>
        public bool IsAvailable(ISBN isbn);
    }
}