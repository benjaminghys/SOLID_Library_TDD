namespace LibraryExercise
{
    /// <summary>
    /// Service for borrowing books.
    /// </summary>
    public interface IBookBorrowingService
    {
        /// <summary>
        /// Attempts to borrow a book from the library.
        /// </summary>
        /// <param name="book">The book to be borrowed.</param>
        /// <returns>
        /// A success result if the book was borrowed successfully.
        /// A failure result if the book is already borrowed or does not exist in the library.
        /// </returns>
        public IResult BorrowBook(Book book);

        /// <summary>
        /// Checks if a book is available for borrowing.
        /// </summary>
        /// <param name="book">The book to check availability for.</param>
        /// <returns>
        /// A success result containing a boolean value indicating availability.
        /// A failure result if the book does not exist in the library.
        /// </returns>
        public IResult<bool> IsBookAvailable(Book book);

        /// <summary>
        /// Returns a borrowed book to the library, making it available for others to borrow.
        /// </summary>
        /// <param name="book">The book to be returned.</param>
        /// <returns>
        /// A success result if the book was returned successfully.
        /// A failure result if the book does not exist in the library or was not borrowed.
        /// </returns>
        public IResult ReturnBook(Book book);
    }
}