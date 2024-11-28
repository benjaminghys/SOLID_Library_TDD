namespace LibraryExerciseTests
{
    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// The <see cref="BookBorrowingService"/> tests.
    /// </summary>
    [TestCategory("Borrowing")]
    [TestCategory("Services")]
    [TestClass]
    public class BookBorrowingServiceTests
    {
        /// <summary>
        /// Borrow an existing and available book from a library.
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
        public void Borrow_existing_book_from_library(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            IResult result = service.BorrowBook(code);

            // Assert
            Assert.AreEqual(true, result.Success, "Borrowing an available and non-borrowed book should always work.");
            Assert.AreEqual(1, service.TotalBorrowed, "Only one book should be borrowed.");
        }

        /// <summary>
        /// Borrowing a book from an empty library should always fail.
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
        public void Borrow_book_from_empty_library(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            IResult result = service.BorrowBook(code);

            // Assert
            Assert.AreEqual(false, result.Success, "Borrowing a book that does not exist should fail.");
            Assert.AreEqual(0, service.TotalBorrowed, "No books should have been borrowed.");
        }

        /// <summary>
        /// Borrowing a borrowed book should always fail.
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
        public void Borrow_book_twice_from_library(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);
            service.BorrowBook(code);

            // Act
            IResult result = service.BorrowBook(code);

            // Assert
            Assert.AreEqual(false, result.Success, "Borrowing a borrowed book should fail.");
            Assert.AreEqual(1, service.TotalBorrowed, "Only one book should be borrowed.");
        }

        /// <summary>
        /// Returning a borrowed book the library should always work.
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
        public void Return_borrowed_book_to_library(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);
            service.BorrowBook(code);

            // Act
            IResult result = service.ReturnBook(code);

            // Assert
            Assert.AreEqual(true, result.Success, "Returning a book that was borrowed should work.");
            Assert.AreEqual(0, service.TotalBorrowed, "The only borrowed book should be returned.");
        }

        /// <summary>
        /// Returning a book to an empty library should always fail.
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
        public void Return_book_to_empty_library(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            IResult result = service.ReturnBook(code);

            // Assert
            Assert.AreEqual(false, result.Success, "Returning a book that does not exist should fail.");
            Assert.AreEqual(0, service.TotalBorrowed, "No books should be borrowed.");
        }

        /// <summary>
        /// Returning a book that is not borrowed should always fail.
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
        public void Return_book_to_library_that_is_not_borrowed(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            IResult result = service.ReturnBook(code);

            // Assert
            Assert.AreEqual(false, result.Success, "Returning a book that is not borrowed should fail.");
            Assert.AreEqual(0, service.TotalBorrowed, "No books should be borrowed.");
        }

        /// <summary>
        /// Borrowing a book that does not exist should always fail.
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
        public void Borrow_non_existent_book_from_library(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            IResult result = service.BorrowBook(code);

            // Assert
            Assert.AreEqual(false, result.Success, "Borrowing a book that does not exist should fail.");
            Assert.AreEqual(0, service.TotalBorrowed, "No books should be borrowed.");
        }

        /// <summary>
        /// Returning a book that does not exist in the library should always fail.
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
        public void Return_non_existent_book(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            IResult result = service.ReturnBook(code);

            // Assert
            Assert.AreEqual(false, result.Success, "Returning a book that does not exist should fail.");
            Assert.AreEqual(0, service.TotalBorrowed, "No books should be borrowed.");
        }

        /// <summary>
        /// A book should always be unavailable after it was borrowed.
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
        public void Check_availability_after_borrowing_book(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);
            service.BorrowBook(code);

            // Act
            bool result = service.IsAvailable(code);

            // Assert
            Assert.AreEqual(false, result, "A books should no longer be available after being borrowed.");
            Assert.AreEqual(1, service.TotalBorrowed, "Only one book should be borrowed.");
        }

        /// <summary>
        /// A book should always be available if it was not borrowed.
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
        public void Check_availability_before_borrowing_book(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);

            // Act
            bool result = service.IsAvailable(code);

            // Assert
            Assert.AreEqual(true, result, "Book is not borrowed and has to be available.");
        }

        /// <summary>
        /// A book should always become available again when it was returned.
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
        public void Check_availability_after_returning_book(string isbn)
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN code = new ISBN(isbn);
            IResult borrowResult = service.BorrowBook(code);
            IResult returnResult = service.ReturnBook(code);

            // Act
            bool result = service.IsAvailable(code);

            // Assert
            Assert.AreEqual(true, borrowResult.Success, "Borrowing an existing book should always work.");
            Assert.AreEqual(true, returnResult.Success, "Returning an existing borrowed book should always work.");
            Assert.AreEqual(true, result, "Book is returned and has to be available again.");
        }
    }
}
