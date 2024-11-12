namespace LibraryExerciseTests
{
    using LibraryExercise;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void Add_a_book_to_the_library()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            Library library = new Library();

            // Act
            IResult result = library.AddBook(book);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(1, library.Count);
        }

        [TestMethod]
        public void Add_the_same_book_twice_to_the_library()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            Library library = new Library();
            library.AddBook(book);

            // Act
            IResult result = library.AddBook(book);

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.AreEqual(1, library.Count);
        }

        [TestMethod]
        public void Remove_a_book_from_the_library_with_the_same_ISBN_but_new_instance()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            Library library = new Library();
            library.AddBook(book);
            Book otherBook = new Book(string.Empty, string.Empty, "0123456789123");

            // Act
            IResult result = library.RemoveBook(otherBook);

            // Assert
            Assert.AreEqual(true, result.Success, result.ToString());
            Assert.AreEqual(0, library.Count);
        }

        [TestMethod]
        public void Remove_a_book_from_the_library_with_the_same_book_instance()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            Library library = new Library();
            library.AddBook(book);

            // Act
            IResult result = library.RemoveBook(book);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(0, library.Count);
        }

        [TestMethod]
        public void Check_if_an_ISBN_exists_using_the_same_book_code()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var book = new Book("0", "0", code);
            var library = new Library();

            library.AddBook(book);

            // Act
            IResult<bool> result = library.IsbnExists(code);

            // Assert
            Assert.AreEqual(code.GetHashCode(), book.GetHashCode());
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(true, result.Value);
        }

        [TestMethod]
        public void Check_if_ISBN_does_not_exist_on_an_empty_library()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var library = new Library();

            // Act
            IResult<bool> result = library.IsbnExists(code);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(false, result.Value);
        }
    }
}