namespace LibraryExercise
{
    /// <summary>
    /// The main class entry point.
    /// </summary>
    internal class MainClass
    {
        /// <summary>
        /// The main entry point of the program.
        /// Gives an example of how to create an <see cref="IBookLibrary"/> and <see cref="IBookBorrowingService"/>.
        /// And how to borrow and return books.
        /// </summary>
        /// <param name="args">Provided command line arguments.</param>
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            // Initialize components
            var library = new BookLibrary();
            IBookLibrary bookLibrary = library;
            IBookAvailabilityChecker availabilityChecker = library;
            IBookBorrowingService borrowingService = new BookBorrowingService(availabilityChecker);

            // Add some sample books
            bookLibrary.AddBook(new Book("1984", "George Orwell", "1234567890123"));
            bookLibrary.AddBook(new Book("Brave New World", "Aldous Huxley", "9876543210987"));

            // Borrow a book
            var isbn = new ISBN("1234567890123");
            if (borrowingService.IsAvailable(isbn))
            {
                borrowingService.BorrowBook(isbn);
                Console.WriteLine("Book borrowed successfully.");
            }

            // Return the book
            var returnResult = borrowingService.ReturnBook(isbn);
            if (!returnResult.Success)
            {
                Console.WriteLine($"Failed to return book: {returnResult.ErrorMessage}");
                return;
            }

            Console.WriteLine("Book returned successfully.");

            // Simple output to demonstrate the library's state
            Console.WriteLine($"Library contains {library.Count} books.");
            Console.WriteLine($"{borrowingService.TotalBorrowed} books have been borrowed.");
        }
    }
}