namespace LibraryExercise;

/// <summary>
///     Library interface for managing books, including adding, removing, and checking for books.
/// </summary>
public interface IBookLibrary
{
    /// <summary>
    ///     Gets the total amount of books in the library
    /// </summary>
    public int Count { get; }

    /// <summary>
    ///     Attempts to add a book to the library.
    /// </summary>
    /// <param name="book">The book to add to the library.</param>
    /// <returns>
    ///     A success result if the book was added successfully.
    ///     A failure result if the book already exists in the library.
    /// </returns>
    public IResult AddBook(Book book);

    /// <summary>
    ///     Attempts to remove a book from the library.
    /// </summary>
    /// <param name="book">The book to remove from the library.</param>
    /// <returns>
    ///     A success result if the book was removed successfully.
    ///     A failure result if the book does not exist in the library.
    /// </returns>
    public IResult RemoveBook(Book book);

    /// <summary>
    ///     Attempts to remove a book by <see cref="ISBN" /> from the library.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to remove.</param>
    /// <returns>
    ///     A success result if the book was removed successfully.
    ///     A failure result if no book with the specified ISBN exists in the library.
    /// </returns>
    public IResult RemoveByIsbn(ISBN isbn);

    /// <summary>
    ///     Checks if a book exists in the library.
    /// </summary>
    /// <param name="book">The book to check for existence.</param>
    /// <returns>
    ///     A success result containing a boolean value indicating if the book exists.
    /// </returns>
    public IResult<bool> BookExists(Book book);

    /// <summary>
    ///     Checks if a book with the specified ISBN exists in the library.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to check for existence.</param>
    /// <returns>
    ///     A success result containing a boolean value indicating if a book with the ISBN exists.
    /// </returns>
    public IResult<bool> IsbnExists(ISBN isbn);
}