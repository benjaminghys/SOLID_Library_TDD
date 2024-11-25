namespace LibraryExerciseTests
{
    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BookLibraryTests
    {
        [TestMethod]
        public void Add_book_to_the_library()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            BookLibrary bookLibrary = new BookLibrary();

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(1, bookLibrary.Count);
        }

        [TestMethod]
        public void Add_same_book_twice_to_library()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            BookLibrary bookLibrary = new BookLibrary();
            bookLibrary.AddBook(book);

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.AreEqual(1, bookLibrary.Count);
        }

        [TestMethod]
        public void Remove_book_from_library_with_same_ISBN_but_new_instance()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            BookLibrary bookLibrary = new BookLibrary();
            bookLibrary.AddBook(book);
            Book otherBook = new Book(string.Empty, string.Empty, "0123456789123");

            // Act
            IResult result = bookLibrary.RemoveBook(otherBook);

            // Assert
            Assert.AreEqual(true, result.Success, result.ToString());
            Assert.AreEqual(0, bookLibrary.Count);
        }

        [TestMethod]
        public void Remove_book_from_library_with_same_book_instance()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            BookLibrary bookLibrary = new BookLibrary();
            bookLibrary.AddBook(book);

            // Act
            IResult result = bookLibrary.RemoveBook(book);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(0, bookLibrary.Count);
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
            IResult<bool> result = library.IsbnExists(code);

            // Assert
            Assert.AreEqual(code.GetHashCode(), book.GetHashCode());
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(true, result.Value);
        }

        [TestMethod]
        public void Check_whether_ISBN_does_not_exist_on_empty_library()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var library = new BookLibrary();

            // Act
            IResult<bool> result = library.IsbnExists(code);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(false, result.Value);
        }
    }
}