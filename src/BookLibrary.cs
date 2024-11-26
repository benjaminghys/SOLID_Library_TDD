namespace LibraryExercise
{
    /// <summary>
    /// Represents a library that stores and manages books.
    /// This class is responsible for adding, removing, and querying books.
    /// It also allows for checking the availability of a book based on its unique ISBN.
    /// Implements <see cref="IBookLibrary"/> for library management and <see cref="IBookAvailabilityChecker"/> for availability checks.
    /// </summary>
    public class BookLibrary : IBookLibrary, IBookAvailabilityChecker
    {
        /// <summary>
        /// Internal collection of all books present in the library.
        /// This collection ensures each book is stored uniquely by using a <see cref="HashSet{T}"/>.
        /// </summary>
        internal readonly HashSet<Book> Books = new ();

        /// <summary>
        /// Gets the total count of books currently in the library.
        /// </summary>
        /// <value>The number of books in the library.</value>
        /// <remarks>
        /// This count reflects the unique number of books in the collection. Books with duplicate ISBNs are not allowed.
        /// </remarks>
        public int Count => Books.Count;

        /// <summary>
        /// Adds a new book to the library.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> to add to the library.</param>
        /// <returns>
        /// A <see cref="IResult"/> indicating whether the operation was successful.
        /// If the book already exists in the library (i.e., a book with the same ISBN is present),
        /// the result will indicate failure.
        /// </returns>
        public IResult AddBook(Book book)
        {
            return !Books.Add(book) 
                       ? Result.FailResult("Book already exists in the library.") 
                       : Result.SuccessResult();
        }

        /// <summary>
        /// Removes a specified book from the library.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> to remove from the library.</param>
        /// <returns>
        /// A <see cref="IResult"/> indicating whether the removal was successful.
        /// Removal is based on the book's unique ISBN; if the book does not exist in the library, the result will indicate failure.
        /// </returns>
        public IResult RemoveBook(Book book)
        {
            return RemoveByIsbn(book.ISBN);
        }

        /// <summary>
        /// Removes a book from the library based on its unique ISBN.
        /// </summary>
        /// <param name="isbn">The unique <see cref="ISBN"/> of the book to remove.</param>
        /// <returns>
        /// A <see cref="IResult"/> indicating the outcome of the removal.
        /// The result will indicate failure if no book with the specified ISBN exists, or if the removal fails.
        /// </returns>
        public IResult RemoveByIsbn(ISBN isbn)
        {
            if (Books.Count(b => b.ISBN.Equals(isbn)) == 0)
            {
                return Result.FailResult("Book does not exist in the library.");
            }

            var success = Books.RemoveWhere(b => b.ISBN.Equals(isbn)) == 1;
            return !success 
                       ? Result.FailResult("Failed to remove book from the library.") 
                       : Result.SuccessResult();
        }

        /// <summary>
        /// Checks if a book with a given ISBN is available in the library.
        /// </summary>
        /// <param name="isbn">The unique <see cref="ISBN"/> of the book to check availability for.</param>
        /// <returns>
        /// A <see cref="IResult{T}"/> containing a boolean value indicating availability.
        /// Returns <c>true</c> if the book is available in the library; otherwise, returns <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method does not differentiate between multiple copies of the same book; 
        /// it simply checks if at least one copy with the specified ISBN is present.
        /// </remarks>
        public IResult<bool> IsAvailable(ISBN isbn)
        {
            return Result<bool>.SuccessResult(Books.Any(b => b.ISBN.Equals(isbn)));
        }
    }
}