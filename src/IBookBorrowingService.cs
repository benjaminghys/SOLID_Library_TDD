namespace LibraryExercise
{
    /// <summary>
    ///     Service for borrowing books.
    /// </summary>
    public interface IBookBorrowingService
    {
        /// <summary>
        ///     Gets the total amount of books borrowed.
        /// </summary>
        public int TotalBorrowed { get; }

        /// <summary>
        ///     Attempts to borrow a book from the library.
        /// </summary>
        /// <param name="isbn">The book <see cref="ISBN"/> to be borrowed.</param>
        /// <returns>
        ///     A success result if the book was borrowed successfully.
        ///     A failure result if the book is already borrowed or does not exist in the library.
        /// </returns>
        public IResult BorrowBook(ISBN isbn);

        /// <summary>
        ///     Returns a borrowed book to the library, making it available for others to borrow.
        /// </summary>
        /// <param name="isbn">The book <see cref="ISBN"/> to be returned.</param>
        /// <returns>
        ///     A success result if the book was returned successfully.
        ///     A failure result if the book does not exist in the library or was not borrowed.
        /// </returns>
        public IResult ReturnBook(ISBN isbn);

        /// <summary>
        ///     Checks if the book is available.
        /// </summary>
        /// <param name="isbn">The book <see cref="ISBN"/> to be retrieved.</param>
        /// <returns>
        ///     A success result if the book was returned successfully.
        ///     A failure result if something went wrong.
        /// </returns>
        public bool IsAvailable(ISBN isbn);
    }
}