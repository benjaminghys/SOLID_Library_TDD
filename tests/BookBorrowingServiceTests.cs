namespace LibraryExerciseTests
{
    using LibraryExercise;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    // ReSharper disable StyleCop.SA1600
    [TestCategory("BookBorrowing")]
    [TestClass]
    public class BookBorrowingServiceTests
    {
        [TestMethod]
        public void Borrow_book_from_library()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            IResult result = service.BorrowBook(isbn);

            // Assert
            Assert.AreEqual(true, result.Success, "Borrowing an existing available book should work.");
        }

        [TestMethod]
        public void Borrow_book_from_empty_library()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            IResult result = service.BorrowBook(isbn);

            // Assert
            Assert.AreEqual(false, result.Success, "Borrowing a book that does not exist should fail.");
        }

        [TestMethod]
        public void Borrow_book_twice_from_library()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");
            service.BorrowBook(isbn);

            // Act
            IResult result = service.BorrowBook(isbn);

            // Assert
            Assert.AreEqual(false, result.Success, "Borrowing a borrowed book should fail.");
        }

        [TestMethod]
        public void Return_borrowed_book_to_library()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");
            IResult borrowResult = service.BorrowBook(isbn);

            // Act
            IResult result = service.ReturnBook(isbn);

            // Assert
            Assert.AreEqual(true, borrowResult.Success, "Borrowing an available book should work.");
            Assert.AreEqual(true, result.Success, "Returning a book that was just borrowed should work.");
        }

        [TestMethod]
        public void Return_book_to_empty_library()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            IResult result = service.ReturnBook(isbn);

            // Assert
            Assert.AreEqual(false, result.Success, "Returning a book that does not exist should fail.");
        }

        [TestMethod]
        public void Return_book_to_library_that_is_not_borrowed()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            IResult result = service.ReturnBook(isbn);

            // Assert
            Assert.AreEqual(false, result.Success, "Returning a book that was not borrowed should fail.");
        }

        [TestMethod]
        public void Borrow_non_existent_book_from_library()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            IResult result = service.BorrowBook(isbn);

            // Assert
            Assert.AreEqual(false, result.Success, "Borrowing a book that does not exist should fail.");
        }

        [TestMethod]
        public void Return_non_existent_book()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(false));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            IResult result = service.ReturnBook(isbn);

            // Assert
            Assert.AreEqual(false, result.Success, "Returning a book that does not exist should fail.");
        }

        [TestMethod]
        public void Check_availability_after_borrowing_book()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");
            IResult borrowResult = service.BorrowBook(isbn);

            // Act
            bool result = service.IsAvailable(isbn);

            // Assert
            Assert.AreEqual(true, borrowResult.Success, "Borrowing an existing book should always work.");
            Assert.AreEqual(false, result, "Borrowing a borrowed book should always fail.");
        }

        [TestMethod]
        public void Check_availability_before_borrowing_book()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");

            // Act
            bool result = service.IsAvailable(isbn);

            // Assert
            Assert.AreEqual(true, result, "Book is not borrowed and has to be available.");
        }

        [TestMethod]
        public void Check_availability_after_returning_book()
        {
            // Arrange
            var libraryMock = new Mock<IBookAvailabilityChecker>();
            libraryMock.Setup(s => s.IsAvailable(It.IsAny<ISBN>()))
                .Returns(Result<bool>.SuccessResult(true));

            IBookBorrowingService service = new BookBorrowingService(libraryMock.Object);
            ISBN isbn = new ISBN("0001112223334");
            IResult borrowResult = service.BorrowBook(isbn);
            IResult returnResult = service.ReturnBook(isbn);

            // Act
            bool result = service.IsAvailable(isbn);

            // Assert
            Assert.AreEqual(true, borrowResult.Success, "Borrowing an existing book should always work.");
            Assert.AreEqual(true, returnResult.Success, "Returning an existing borrowed book should always work.");
            Assert.AreEqual(true, result, "Book is not borrowed and has to be available.");
        }
    }
}
