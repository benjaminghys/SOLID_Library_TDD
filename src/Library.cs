namespace LibraryExercise
{
    using System.Diagnostics;

    /// <summary>
    /// Represents a library system that manages books, allowing users to add, remove and check the existence.
    /// As well as borrowing, returning and checking availability of a book.
    /// </summary>
    /// <remarks>
    /// Implements both <see cref="IBookLibrary"/> and <see cref="IBookBorrowingService"/> 
    /// to provide complete management and borrowing functionality for <see cref="Book"/> objects.
    /// This class can be used to perform operations on a library's collection, 
    /// as well as to handle user borrowing activity.
    /// </remarks>
    public class Library : IBookLibrary, IBookBorrowingService
    {
        /// <summary>
        /// Internal list of all books present in the library.
        /// </summary>
        internal readonly HashSet<Book> Books = new ();

        /// <summary>
        /// Hash set of all borrowed book <see cref="ISBN"/> codes.
        /// </summary>
        internal readonly HashSet<Book> Borrowed = new ();

        /// <inheritdoc cref="IBookLibrary.Count"/>
        public int Count => this.Books.Count;

        /// <inheritdoc cref="IBookLibrary.AddBook"/>
        public IResult AddBook(Book book)
        {
            return !this.Books.Add(book) ? Result.FailResult("Book already exists in the library.") : Result.SuccessResult();
        }

        /// <inheritdoc cref="IBookLibrary.RemoveBook"/>
        public IResult RemoveBook(Book book)
        {
            if (this.Books.Count(b => b.ISBN.Equals(book.ISBN)) == 0)
            {
                return Result.FailResult("Book does not exist in the library.");
            }

            bool success = this.Books.RemoveWhere(b => b.ISBN.Equals(book.ISBN)) == 1;
            return !success ? Result.FailResult("Failed to remove book from the library.") : Result.SuccessResult();
        }

        /// <inheritdoc cref="IBookLibrary.RemoveByIsbn"/>
        public IResult RemoveByIsbn(ISBN isbn)
        {
            return RemoveBook(new Book(string.Empty, string.Empty, isbn));
        }

        /// <inheritdoc cref="IBookLibrary.BookExists"/>
        public IResult<bool> BookExists(Book book)
        {
            return Result<bool>.SuccessResult(this.Books.Contains(book));
        }

        /// <inheritdoc cref="IBookLibrary.IsbnExists"/>
        public IResult<bool> IsbnExists(ISBN isbn)
        {
            return Result<bool>.SuccessResult(this.Books.Any(b => b.ISBN.Equals(isbn)));
        }

        /// <inheritdoc cref="IBookBorrowingService.BorrowBook"/>
        public IResult BorrowBook(Book book)
        {
            return !this.Borrowed.Add(book) ? Result.FailResult("Book was already borrowed.") : Result.SuccessResult();
        }

        /// <inheritdoc cref="IBookBorrowingService.IsBookAvailable"/>
        public IResult<bool> IsBookAvailable(Book book)
        {
            return Result<bool>.SuccessResult(!this.Borrowed.Contains(book));
        }

        /// <inheritdoc cref="IBookBorrowingService.ReturnBook"/>
        public IResult ReturnBook(Book book)
        {
            return !this.Borrowed.Remove(book) ? Result.FailResult("Book was not borrowed.") : Result.SuccessResult();
        }
    }
}