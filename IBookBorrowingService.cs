namespace LibraryExercise
{
    public interface IBookBorrowingService
    {
        public void BorrowBook(Book book);

        public bool IsBookAvailable(Book book);

        public void ReturnBook(Book book);
    }
}