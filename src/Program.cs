using LibraryExercise;

internal class MainClass
{
    private static void Main(string[] args)
    {
        // Initialize components
        var library = new BookLibrary();
        var borrowingService = new BookBorrowingService(library);

        // Add some sample books
        library.AddBook(new Book("1984", "George Orwell", "1234567890123"));
        library.AddBook(new Book("Brave New World", "Aldous Huxley", "9876543210987"));

        // Borrow a book
        var isbn = new ISBN("1234567890123");
        var result = borrowingService.IsBookAvailable(isbn);
        if (!result.Success)
        {
            Console.WriteLine($"Failed to check if book is available: {result.ErrorMessage}");
            return;
        }

        if (result.Value)
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
        Console.WriteLine($"{borrowingService.Borrowed.Count} books have been borrowed.");
    }
}