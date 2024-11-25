namespace LibraryExercise
{
    public class BookBorrowingService(IBookLibrary library) : IBookBorrowingService
    {
        public IBookLibrary Library { get; } = library;

        /// <summary>
        ///     Hash set of all borrowed book <see cref="ISBN" /> codes.
        /// </summary>
        internal readonly HashSet<ISBN> Borrowed = [];

        /// <inheritdoc cref="IBookBorrowingService.TotalBorrowed"/>
        public int TotalBorrowed => Borrowed.Count;

        /// <inheritdoc cref="IBookBorrowingService.BorrowBook" />
        public IResult BorrowBook(ISBN isbn)
        {
            IResult<bool> result = Library.IsbnExists(isbn);
            if (!result.Success) return Result.FailResult($"Failed to borrow book: {result.ErrorMessage}");

            if (!result.Value)
            {
                return Result.FailResult("Book does not exist in the library.");
            }
            return !Borrowed.Add(isbn)
                ? Result.FailResult("Book was already borrowed.")
                : Result.SuccessResult();
        }

        /// <inheritdoc cref="IBookBorrowingService.IsBookAvailable" />
        public IResult<bool> IsBookAvailable(ISBN isbn)
        {
            return Result<bool>.SuccessResult(!Borrowed.Contains(isbn));
        }

        /// <inheritdoc cref="IBookBorrowingService.ReturnBook" />
        public IResult ReturnBook(ISBN isbn)
        {
            IResult<bool> result = Library.IsbnExists(isbn);
            if (!result.Success) return Result.FailResult($"Failed to return book: {result.ErrorMessage}");

            if (!result.Value)
            {
                return Result.FailResult("Book does not exist in the library.");
            }
            return !Borrowed.Remove(isbn) 
                ? Result.FailResult("Book was not borrowed.") 
                : Result.SuccessResult();
        }
    }
}