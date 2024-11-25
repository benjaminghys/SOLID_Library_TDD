namespace LibraryExercise
{
    public class BookLibrary : IBookLibrary
    {
        /// <summary>
        ///     Internal list of all books present in the library.
        /// </summary>
        internal readonly HashSet<Book> Books = [];

        /// <inheritdoc cref="IBookLibrary.Count" />
        public int Count => Books.Count;

        /// <inheritdoc cref="IBookLibrary.AddBook" />
        public IResult AddBook(Book book)
        {
            return !Books.Add(book) ? Result.FailResult("Book already exists in the library.") : Result.SuccessResult();
        }

        /// <inheritdoc cref="IBookLibrary.RemoveBook" />
        public IResult RemoveBook(Book book)
        {
            if (Books.Count(b => b.ISBN.Equals(book.ISBN)) == 0)
                return Result.FailResult("Book does not exist in the library.");

            var success = Books.RemoveWhere(b => b.ISBN.Equals(book.ISBN)) == 1;
            return !success ? Result.FailResult("Failed to remove book from the library.") : Result.SuccessResult();
        }

        /// <inheritdoc cref="IBookLibrary.RemoveByIsbn" />
        public IResult RemoveByIsbn(ISBN isbn)
        {
            return RemoveBook(new Book(string.Empty, string.Empty, isbn));
        }

        /// <inheritdoc cref="IBookLibrary.BookExists" />
        public IResult<bool> BookExists(Book book)
        {
            return Result<bool>.SuccessResult(Books.Contains(book));
        }

        /// <inheritdoc cref="IBookLibrary.IsbnExists" />
        public IResult<bool> IsbnExists(ISBN isbn)
        {
            return Result<bool>.SuccessResult(Books.Any(b => b.ISBN.Equals(isbn)));
        }
    }
}