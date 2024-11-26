namespace LibraryExercise
{
    /// <summary>
    /// Represents a service for managing book borrowing operations.
    /// This service allows books to be borrowed and returned while keeping track of borrowed books.
    /// Implements <see cref="IBookBorrowingService"/>.
    /// </summary>
    public class BookBorrowingService : IBookBorrowingService
    {
        /// <summary>
        /// Provides the functionality to check the availability of a <see cref="Book"/> in the library.
        /// </summary>
        private readonly IBookAvailabilityChecker _availabilityChecker;

        /// <summary>
        /// A collection that keeps track of all borrowed book <see cref="ISBN"/> codes.
        /// Used to ensure books cannot be borrowed multiple times without being returned.
        /// </summary>
        private readonly HashSet<ISBN> _borrowed = new ();

        /// <summary>
        /// Gets the total number of borrowed books.
        /// </summary>
        /// <value>The count of currently borrowed books.</value>
        public int TotalBorrowed => this._borrowed.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookBorrowingService"/> class.
        /// </summary>
        /// <param name="availabilityChecker">
        /// An interface used to check if books are available for borrowing or returning.
        /// </param>
        public BookBorrowingService(IBookAvailabilityChecker availabilityChecker)
        {
            this._availabilityChecker = availabilityChecker;
        }

        /// <summary>
        /// Attempts to borrow a book using its unique ISBN.
        /// </summary>
        /// <param name="isbn">The <see cref="ISBN"/> of the book to borrow.</param>
        /// <returns>
        /// A <see cref="IResult"/> indicating whether the borrowing operation was successful.
        /// If the book does not exist, or if it has already been borrowed, the result will indicate failure.
        /// </returns>
        public IResult BorrowBook(ISBN isbn)
        {
            var result = this._availabilityChecker.IsAvailable(isbn);
            if (!result.Success)
            {
                return Result.FailResult(string.Concat("Failed to borrow book:", result.ErrorMessage));
            }

            if (!result.Value)
            {
                return Result.FailResult("Book does not exist in the library.");
            }

            return !this._borrowed.Add(isbn)
                ? Result.FailResult("Book was already borrowed.")
                : Result.SuccessResult();
        }

        /// <summary>
        /// Attempts to return a book using its unique ISBN.
        /// </summary>
        /// <param name="isbn">The <see cref="ISBN"/> of the book to return.</param>
        /// <returns>
        /// A <see cref="IResult"/> indicating whether the return operation was successful.
        /// If the book does not exist in the library, or if it was not borrowed, the result will indicate failure.
        /// </returns>
        public IResult ReturnBook(ISBN isbn)
        {
            IResult<bool> result = this._availabilityChecker.IsAvailable(isbn);
            if (!result.Success)
            {
                return Result.FailResult(string.Concat("Failed to return book: ", result.ErrorMessage));
            }

            if (!result.Value)
            {
                return Result.FailResult("Book does not exist in the library.");
            }

            return !this._borrowed.Remove(isbn) 
                ? Result.FailResult("Book was not borrowed.") 
                : Result.SuccessResult();
        }

        /// <summary>
        /// Checks if a book with the given <see cref="ISBN"/> is currently available for borrowing.
        /// </summary>
        /// <param name="isbn">The unique <see cref="ISBN"/> code of the book to check availability for.</param>
        /// <returns>
        /// <c>true</c> if the book exists in the library and has not yet been borrowed; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method leverages the <see cref="IBookAvailabilityChecker"/> to determine the existence of the book.
        /// It will return false if the book is either not in the library or is currently borrowed.
        /// </remarks>
        public bool IsAvailable(ISBN isbn)
        {
            var result = this._availabilityChecker.IsAvailable(isbn);
            return result.Success && result.Value && !_borrowed.Contains(isbn);
        }
    }
}