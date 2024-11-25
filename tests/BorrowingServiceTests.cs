namespace LibraryExerciseTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LibraryExercise;

    [TestClass]
    public class BorrowingServiceTests
    {
        [TestMethod]
        public void Borrow_book_from_library()
        {
            // Arrange
            IBookLibrary library = new BookLibrary();
            Book book = new Book("Test", "Author", new ISBN("9781234567897"));
            library.AddBook(book);

            IBookBorrowingService service = new BookBorrowingService(library);

            // Act
            IResult result = service.BorrowBook(book.ISBN);

            // Assert
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void Borrow_book_from_empty_library()
        {
            // Arrange
            IBookLibrary library = new BookLibrary();
            Book book = new Book("Test", "Author", new ISBN("9781234567897"));

            IBookBorrowingService service = new BookBorrowingService(library);

            // Act
            IResult result = service.BorrowBook(book.ISBN);

            // Assert
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void Borrow_book_twice_from_library()
        {
            // Arrange
            var library = new BookLibrary();
            var book = new Book("Test", "Author", new ISBN("9781234567897"));
            library.AddBook(book);

            IBookBorrowingService service = new BookBorrowingService(library);
            service.BorrowBook(book.ISBN);

            // Act
            IResult result = service.BorrowBook(book.ISBN);

            // Assert
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void Return_book_to_library()
        {
            // Arrange
            var library = new BookLibrary();
            var book = new Book("Test", "Author", new ISBN("9781234567897"));
            library.AddBook(book);

            IBookBorrowingService service = new BookBorrowingService(library);
            service.BorrowBook(book.ISBN);

            // Act
            IResult result = service.ReturnBook(book.ISBN);

            // Assert
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void Return_book_to_empty_library()
        {
            // Arrange
            var library = new BookLibrary();
            var book = new Book("Test", "Author", new ISBN("9781234567897"));

            IBookBorrowingService service = new BookBorrowingService(library);
            service.BorrowBook(book.ISBN);

            // Act
            IResult result = service.ReturnBook(book.ISBN);

            // Assert
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void Return_book_to_library_that_is_not_borrowed()
        {
            // Arrange
            var library = new BookLibrary();
            var book = new Book("Test", "Author", new ISBN("9781234567897"));
            library.AddBook(book);

            IBookBorrowingService service = new BookBorrowingService(library);

            // Act
            IResult result = service.ReturnBook(book.ISBN);

            // Assert
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void Borrow_non_existent_book_from_library()
        {
            // Arrange
            var library = new BookLibrary();
            var book = new Book("Test", "Author", new ISBN("9781234567897"));
            var otherBook = new Book("Test", "Author", new ISBN("0001234567000"));
            library.AddBook(book);

            IBookBorrowingService service = new BookBorrowingService(library);

            // Act
            IResult result = service.BorrowBook(otherBook.ISBN);

            // Assert
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void Return_non_existent_book_from_library()
        {
            // Arrange
            var library = new BookLibrary();
            var book = new Book("Test", "Author", new ISBN("9781234567897"));
            var otherBook = new Book("Test", "Author", new ISBN("0001234567000"));
            library.AddBook(book);

            IBookBorrowingService service = new BookBorrowingService(library);

            // Act
            IResult result = service.ReturnBook(otherBook.ISBN);

            // Assert
            Assert.AreEqual(false, result.Success);
        }
    }
}
