namespace LibraryExerciseTests
{
    using System;
    using System.Collections.Generic;

    using LibraryExercise;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        void Test_AddBook()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            Library library = new Library();

            // Act
            library.AddBook(book);

            // Assert
            Assert.AreEqual(1, library.Count);
        }

        [TestMethod]
        void Test_AddBook_NonUnique()
        {
            // Arrange
            Book book = new Book("Test", "Tester", "0123456789123");
            Library library = new Library();
            library.AddBook(book);

            // Act
            library.AddBook(book); // Book should not be added.

            // Assert
            Assert.AreEqual(1, library.Count);
        }

        [TestMethod]
        void Test_ISBNExists_Success()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var book = new Book("0", "0", code);
            var library = new Library();

            library.AddBook(book);

            // Act
            bool result = library.IsbnExists(code);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        void Test_IsbnExists_Fail()
        {
            // Arrange
            var code = new ISBN("1234567891234");
            var library = new Library();

            // Act
            bool result = library.IsbnExists(code);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}