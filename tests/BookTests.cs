#pragma warning disable CS8604 // Possible null reference argument.
namespace LibraryExerciseTests
{
    using System;
    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable StyleCop.SA1600
    [TestCategory("Books")]
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void Create_book_with_valid_arguments()
        {
            _ = new Book("name", "author", "0001112223334");
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_book_with_invalid_name(string? invalidName)
        {
            _ = new Book(invalidName, "author", "0001112223334");
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_book_with_invalid_author(string? invalidAuthor)
        {
            _ = new Book("name", invalidAuthor, "0001112223334");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("a")]
        [DataRow(".")]
        [DataRow("000111222333")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_book_with_invalid_isbn(string isbn)
        {
            _ = new Book("name", "author", isbn);
        }
    }
}