#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. (InitializeTest takes of initializing fields)
namespace LibraryExerciseTests
{
    using System.Collections.Generic;
    using System.Linq;

    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The <see cref="BookLibrary"/> tests.
    /// </summary>
    [TestCategory("Libraries")]
    [TestClass]
    public class BookLibraryTests
    {
        /// <summary>
        /// Gets an empty <see cref="IBookLibrary"/> containing no books.
        /// </summary>
        private BookLibrary _emptyBookLibrary;

        /// <summary>
        /// Gets an <see cref="IBookLibrary"/> contaning multiple books.
        /// </summary>
        private BookLibrary _multiBookLibrary;

        /// <summary>
        /// Internal <see cref="IReadOnlyList{T}"/> of books.
        /// </summary>
        private IReadOnlyList<Book> _books;

        /// <summary>
        /// Initializes all properties before each test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            _books = new List<Book>
                         {
                             new Book("Sample 01", "Author A", "0000000000000"),
                             new Book("Sample 02", "Author B", "0000000000001"),
                             new Book("Sample 03", "Author A", "0000000000002"),
                             new Book("Sample 04", "Author B", "0000000000003"),
                             new Book("Sample 05", "Author A", "0000000000004"),
                             new Book("Sample 06", "Author B", "0000000000005"),
                             new Book("Sample 07", "Author A", "0000000000006"),
                         };

            _emptyBookLibrary = new BookLibrary();
            _multiBookLibrary = new BookLibrary(_books);
        }

        /// <summary>
        /// Adding a book to an empty library should always work.
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
        public void Add_book_to_empty_library(string name, string author, string isbn)
        {
            // Arrange
            Book book = new Book(name, author, isbn);
            IBookLibrary bookLibrary = new BookLibrary(_emptyBookLibrary);
            int previousCount = bookLibrary.Count;

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(true, result.Success, $"Adding a book to an empty library should always work: {result.ErrorMessage}");
            Assert.AreEqual(0, previousCount, "The library should be empty.");
            Assert.AreEqual(1, bookLibrary.Count, "Only 1 book should be added.");
        }

        /// <summary>
        /// Adding a new book to an existing library should always work.
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
        public void Add_book_to_existing_library(string name, string author, string isbn)
        {
            // Arrange
            Book book = new Book(name, author, isbn);
            IBookLibrary bookLibrary = new BookLibrary(_multiBookLibrary);
            int previousCount = bookLibrary.Count;

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(true, result.Success, $"Adding a unique book to an existing library should always work: {result.ErrorMessage}");
            Assert.AreNotEqual(0, bookLibrary.Count, "The library cannot be empty after adding a book.");
            Assert.AreEqual(bookLibrary.Count, previousCount + 1, "The book was not added to the library.");
        }

        /// <summary>
        /// Adding the same book twice to the same empty library should always fail.
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
        public void Add_same_book_twice_to_an_empty_library(string name, string author, string isbn)
        {
            // Arrange
            Book book = new Book(name, author, isbn);
            IBookLibrary bookLibrary = new BookLibrary(_emptyBookLibrary);
            bookLibrary.AddBook(book);

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(false, result.Success, "Adding the same book twice should always fail.");
            Assert.AreEqual(1, bookLibrary.Count, "Only 1 book should be added, as the ISBN needs to be unique.");
        }

        /// <summary>
        /// Adding the same book twice to an existing library should always fail.
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
        public void Add_same_book_twice_to_an_existing_library(string name, string author, string isbn)
        {
            // Arrange
            Book book = new Book(name, author, isbn);
            IBookLibrary bookLibrary = new BookLibrary(_multiBookLibrary);
            int initialCount = bookLibrary.Count;
            bookLibrary.AddBook(book);
            int previousCount = bookLibrary.Count;

            // Act
            IResult result = bookLibrary.AddBook(book);

            // Assert
            Assert.AreEqual(false, result.Success, "Adding the same book twice should always fail.");
            Assert.AreEqual(previousCount, bookLibrary.Count, "No new books should have been added to the library.");
            Assert.AreNotEqual(initialCount, previousCount, "At least one book should have been added to the library.");
        }

        /// <summary>
        /// Removing a different book with the same ISBN code should not fail, but it will remove the original book.
        /// ISBN codes needs to be unique for every book.
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
        public void Remove_book_from_existing_library_with_same_ISBN_but_new_instance(string name, string author, string isbn)
        {
            // Arrange
            Book originalBook = new Book(name, author, isbn);
            Book otherBook = new Book($"Other {name}", $"Other {author}", isbn);

            BookLibrary bookLibrary = new BookLibrary(_multiBookLibrary);
            bookLibrary.AddBook(originalBook);
            int previousCount = bookLibrary.Count;

            // Act
            IResult result = bookLibrary.RemoveBook(otherBook);

            // Assert
            Assert.AreEqual(true, result.Success, $"Removing a book with the same ISBN should always work: {result.ErrorMessage}");
            Assert.AreEqual(previousCount, bookLibrary.Count + 1, "One book should have been removed from the library.");
            Assert.AreEqual(false, bookLibrary.IsAvailable(originalBook.ISBN).Value, "Original book should no longer be available.");
        }

        /// <summary>
        /// Adding and then removing the same book should always work.
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
        public void Add_and_remove_same_book_from_empty_library_with_same_instance(string name, string author, string isbn)
        {
            // Arrange
            Book book = new Book(name, author, isbn);
            IBookLibrary bookLibrary = new BookLibrary();
            int initialCount = bookLibrary.Count;
            bookLibrary.AddBook(book);
            int previousCount = bookLibrary.Count;

            // Act
            IResult result = bookLibrary.RemoveBook(book);

            // Assert
            Assert.AreEqual(true, result.Success, $"Removing the same identical book should always work: {result.ErrorMessage}");
            Assert.AreEqual(0, initialCount, "Library should start empty.");
            Assert.AreEqual(1, previousCount, "Only one book should exist before it's being removed.");
            Assert.AreEqual(0, bookLibrary.Count, "No books should remain in the library.");
        }

        /// <summary>
        /// Removing the same book that was just added should always work.
        /// In this test the same instance will be used, but on the <see cref="ISBN"/> code matters.
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
        public void Remove_book_from_existing_library_with_same_book(string name, string author, string isbn)
        {
            // Arrange
            Book book = new Book(name, author, isbn);
            IBookLibrary bookLibrary = new BookLibrary(_multiBookLibrary);
            int initialCount = bookLibrary.Count;
            bookLibrary.AddBook(book);
            int previousCount = bookLibrary.Count;

            // Act
            IResult result = bookLibrary.RemoveBook(book);

            // Assert
            Assert.AreEqual(true, result.Success, $"Removing the same identical book should always work: {result.ErrorMessage}");
            Assert.AreEqual(initialCount + 1, previousCount, "A book should have been added to the library");
            Assert.AreNotEqual(0, initialCount, "Library should be empty from the start.");
            Assert.AreEqual(initialCount, bookLibrary.Count, "Library should be back to it's initial count after book was removed.");
        }

        /// <summary>
        /// Using a book or the same ISBN code as the book should both work.
        /// </summary>
        /// <param name="isbn">ISBN code of the book.</param>
        [TestMethod]
        [DataRow("9789966566003")]
        [DataRow("9781728353227")]
        [DataRow("9781925435962")]
        [DataRow("9781644247389")]
        [DataRow("9781460251584")]
        [DataRow("9781602660229")]
        [DataRow("9781035902729")]
        [DataRow("9781869712648")]
        [DataRow("9780985756147")]
        public void Check_whether_ISBN_exists_using_same_book_code(string isbn)
        {
            // Arrange
            var code = new ISBN(isbn);
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

        /// <summary>
        /// Using a book with the same ISBN code should have the same hash codes.
        /// </summary>
        /// <param name="isbn">ISBN code of the book.</param>
        [TestMethod]
        [DataRow("9789966566003")]
        [DataRow("9781728353227")]
        [DataRow("9781925435962")]
        [DataRow("9781644247389")]
        [DataRow("9781460251584")]
        [DataRow("9781602660229")]
        [DataRow("9781035902729")]
        [DataRow("9781869712648")]
        [DataRow("9780985756147")]
        public void ISBN_and_book_with_same_ISBN_hash_codes_are_equal(string isbn)
        {
            // Arrange
            var isbnCode = new ISBN(isbn);
            var book = new Book("0", "0", new ISBN(isbn));

            // Act
            int bookHashCode = book.GetHashCode();
            int isbnHashCode = isbnCode.GetHashCode();

            // Assert
            Assert.AreEqual(bookHashCode, isbnHashCode, "Books are hashed by their ISBN code and should be equal.");
        }

        /// <summary>
        /// Using a book with the same ISBN code should have the same hash codes.
        /// </summary>
        /// <param name="isbn">ISBN code of the book.</param>
        [TestMethod]
        [DataRow("9789966566003")]
        [DataRow("9781728353227")]
        [DataRow("9781925435962")]
        [DataRow("9781644247389")]
        [DataRow("9781460251584")]
        [DataRow("9781602660229")]
        [DataRow("9781035902729")]
        [DataRow("9781869712648")]
        [DataRow("9780985756147")]
        public void ISBN_and_book_with_different_ISBN_hash_codes_are_not_equal(string isbn)
        {
            // Arrange
            var isbnCode = new ISBN(isbn);
            var book = _books.First();

            // Act
            int bookHashCode = book.GetHashCode();
            int isbnHashCode = isbnCode.GetHashCode();

            // Assert
            Assert.AreNotEqual(bookHashCode, isbnHashCode, "A book with a different ISBN code should not have the same hash code.");
        }

        /// <summary>
        /// <see cref="IBookAvailabilityChecker.IsAvailable"/> should always return <c>false</c> on an empty library.
        /// </summary>
        /// <param name="isbn">ISBN code of the book.</param>
        [TestMethod]
        [DataRow("9789966566003")]
        [DataRow("9781728353227")]
        [DataRow("9781925435962")]
        [DataRow("9781644247389")]
        [DataRow("9781460251584")]
        [DataRow("9781602660229")]
        [DataRow("9781035902729")]
        [DataRow("9781869712648")]
        [DataRow("9780985756147")]
        public void ISBN_is_not_available_on_empty_library(string isbn)
        {
            // Arrange
            var code = new ISBN(isbn);
            IBookAvailabilityChecker library = new BookLibrary();

            // Act
            IResult<bool> result = library.IsAvailable(code);

            // Assert
            Assert.AreEqual(true, result.Success, "Checking if a valid ISBN code exists on an empty library should always succeed.");
            Assert.AreEqual(false, result.Value, "An empty library should always return false.");
        }

        /// <summary>
        /// <see cref="IBookAvailabilityChecker.IsAvailable"/> should always return <c>false</c> on an empty library.
        /// </summary>
        /// <param name="isbn">ISBN code of the book.</param>
        [TestMethod]
        [DataRow("9789966566003")]
        [DataRow("9781728353227")]
        [DataRow("9781925435962")]
        [DataRow("9781644247389")]
        [DataRow("9781460251584")]
        [DataRow("9781602660229")]
        [DataRow("9781035902729")]
        [DataRow("9781869712648")]
        [DataRow("9780985756147")]
        public void ISBN_is_available_on_existing_library(string isbn)
        {
            // Arrange
            var code = new ISBN(isbn);
            var book = new Book("_", "_", isbn);
            BookLibrary library = new BookLibrary();
            library.AddBook(book);

            // Act
            IResult<bool> result = library.IsAvailable(code);

            // Assert
            Assert.AreEqual(true, result.Success, result.ErrorMessage);
            Assert.AreEqual(true, result.Value, "The ISBN code should be available.");
        }
    }
}