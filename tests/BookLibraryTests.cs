namespace LibraryExerciseTests
{
    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable StyleCop.SA1600
    [TestCategory("BookLibrary")]
    [TestClass]
    public class BookLibraryTests
    {
        [TestMethod]
        public void Add_book_to_the_library()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            IBookLibrary bookLibrary = new BookLibrary();

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(true, result.Success, $"Adding a book to an empty library should always work: {result.ErrorMessage}");
            Assert.AreEqual(1, bookLibrary.Count, "Only 1 book was added.");
        }

        [TestMethod]
        public void Add_same_book_twice_to_library()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            IBookLibrary bookLibrary = new BookLibrary();
            bookLibrary.AddBook(book);

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(false, result.Success, "Adding the same book twice should always fail.");
            Assert.AreEqual(1, bookLibrary.Count, "Only 1 book should exist, as the ISBN should be unique.");
        }

        [TestMethod]
        public void Remove_book_from_library_with_same_ISBN_but_new_instance()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            IBookLibrary bookLibrary = new BookLibrary();
            bookLibrary.AddBook(book);
            Book otherBook = new Book("other", "author", "0123456789123");

            // Act
            IResult result = bookLibrary.RemoveBook(otherBook);

            // Assert
            Assert.AreEqual(true, result.Success, $"Removing a book with the same ISBN should always work: {result.ErrorMessage}");
            Assert.AreEqual(0, bookLibrary.Count, "Removed the only book from the library");
        }

        [TestMethod]
        public void Remove_book_from_library_with_same_book_instance()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            IBookLibrary bookLibrary = new BookLibrary();
            bookLibrary.AddBook(book);

            // Act
            IResult result = bookLibrary.RemoveBook(book);

            // Assert
            Assert.AreEqual(true, result.Success, $"Removing the same identical book should always work: {result.ErrorMessage}");
            Assert.AreEqual(0, bookLibrary.Count, "Removed the only book from the library");
        }

        [TestMethod]
        public void Check_whether_ISBN_exists_using_same_book_code()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var book = new Book("0", "0", code);
            var library = new BookLibrary();

            library.AddBook(book);

            // Act
            IResult<bool> result = library.IsAvailable(code);

            // Assert
            Assert.AreEqual(code.GetHashCode(), book.GetHashCode(), "Books are hashed by their ISBN code.");
            Assert.AreEqual(true, result.Success, "Using the same ISBN code to check if a book exists should work.");
            Assert.AreEqual(true, result.Value, "The result of the function should be true.");
        }

        [TestMethod]
        public void Check_whether_ISBN_does_not_exist_on_empty_library()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var library = new BookLibrary();

            // Act
            IResult<bool> result = library.IsAvailable(code);

            // Assert
            Assert.AreEqual(true, result.Success, "checking if a book exists should work.");
            Assert.AreEqual(false, result.Value, "An empty library should always return false.");
        }
    }
}