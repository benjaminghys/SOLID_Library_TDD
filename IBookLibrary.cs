namespace LibraryExercise
{
    public interface IBookLibrary
    {
        public void AddBook(Book book);

        public void RemoveBook(Book book);

        public void RemoveByIsbn(ISBN isbn);

        public bool BookExists(Book book);

        public bool IsbnExists(ISBN isbn);
    }
}