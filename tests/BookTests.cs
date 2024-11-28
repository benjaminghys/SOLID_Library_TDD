#pragma warning disable CS8604 // Possible null reference argument.
namespace LibraryExerciseTests
{
    using System;
    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The <see cref="Book"/> tests.
    /// </summary>
    [TestCategory("Books")]
    [TestCategory("Data Classes")]
    [TestClass]
    public class BookTests
    {
        /// <summary>
        /// Initialize the <see cref="Book"/> class with valid data.
        /// </summary>
        /// <param name="name">Name of the book.</param>
        /// <param name="author">Author of the book.</param>
        /// <param name="isbn">ISBN code of the book.</param>
        [TestMethod]
        [DataRow("name", "author", "0001112223334")]
        [DataRow("Benji's Big Win", "Nducu wa", "9789966566003")]
        [DataRow("Benji of Bearsden", "Ferguson MacLeod", "9781728353227")]
        [DataRow("Grover, Benji and Nanna Jean", "Claire Garth", "9781925435962")]
        [DataRow("Benji's Allergies and Pets", "Linda Bocian", "9781644247389")]
        [DataRow("Benji Bat Wears Glasses", "Darlene Hartford", "9781460251584")]
        [DataRow("Benji's Sicarii Sword", "Marian Clark", "9781602660229")]
        [DataRow("Benji's Emerald King", "Ewa Jozefkowicz", "9781035902729")]
        [DataRow("Benji My Story", "Hachette New Zealand", "9781869712648")]
        [DataRow("Benji, The No One, The Loser, The Rejected, The Revenge Artist", "Kimberly J Fuller", "9780985756147")]
        public void Create_book_with_valid_arguments(string name, string author, string isbn)
        {
            _ = new Book(name, author, isbn);
        }

        /// <summary>
        /// Initialize the <see cref="Book"/> class with an invalid name.
        /// </summary>
        /// <param name="invalidName">Invalid name.</param>
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("   ")]
        [DataRow("\t")]
        [DataRow("\r")]
        [DataRow("\n")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_book_with_invalid_name(string? invalidName)
        {
            _ = new Book(invalidName, "author", "0001112223334");
        }

        /// <summary>
        /// Initialize the <see cref="Book"/> class with an invalid author.
        /// </summary>
        /// <param name="invalidAuthor">Invalid author.</param>
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("   ")]
        [DataRow("\t")]
        [DataRow("\r")]
        [DataRow("\n")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_book_with_invalid_author(string? invalidAuthor)
        {
            _ = new Book("name", invalidAuthor, "0001112223334");
        }

        /// <summary>
        /// Initialize the <see cref="Book"/> class with an invalid ISBN code.
        /// </summary>
        /// <param name="isbn">Invalid ISBN code.</param>
        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("   ")]
        [DataRow("\t")]
        [DataRow("\r")]
        [DataRow("\n")]
        [DataRow("a")]
        [DataRow(".")]
        [DataRow("0")]
        [DataRow("00")]
        [DataRow("000")]
        [DataRow("0001")]
        [DataRow("00011")]
        [DataRow("000111")]
        [DataRow("0001112")]
        [DataRow("00011122")]
        [DataRow("000111222")]
        [DataRow("0001112223")]
        [DataRow("00011122233")]
        [DataRow("000111222333")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_book_with_invalid_isbn(string isbn)
        {
            _ = new Book("name", "author", isbn);
        }
    }
}