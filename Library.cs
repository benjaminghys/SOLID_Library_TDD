namespace LibraryExercise
{
    public class Library : IBookLibrary, IBookBorrowingService
    {

        private List<Book> m_Books;

        private HashSet<ISBN> m_Isbns;

        private HashSet<ISBN> m_Borrowed;

        public int Count => m_Books.Count;

        public Library()
        {
            this.m_Books = new List<Book>();
            this.m_Isbns = new HashSet<ISBN>();
            this.m_Borrowed = new HashSet<ISBN>();
        }

        public void AddBook(Book book)
        {
            if (this.m_Isbns.Contains(book.ISBN))
            {
                return;
            }

            this.m_Books.Add(book);
            this.m_Isbns.Add(book.ISBN);
        }

        public void RemoveBook(Book book)
        {
            if (!this.m_Isbns.Contains(book.ISBN))
            {
                return;
            }

            this.m_Books.Remove(book);
            this.m_Isbns.Remove(book.ISBN);
        }

        public void RemoveByIsbn(ISBN isbn)
        {
            if (!this.m_Isbns.Contains(isbn))
            {
                return;
            }

            this.m_Books.RemoveAll(b => b.ISBN.Equals(isbn));
            this.m_Isbns.Remove(isbn);
        }

        public bool BookExists(Book book)
        {
            return this.m_Books.Contains(book);
        }

        public bool IsbnExists(ISBN isbn)
        {
            return this.m_Isbns.Contains(isbn);
        }

        public void BorrowBook(Book book)
        {
            if (!this.m_Borrowed.Add(book.ISBN))
            {
                throw new Exception($"Book with ISBN: {book.ISBN} has already been borrowed!");
            }
        }

        public bool IsBookAvailable(Book book)
        {
            return !this.m_Borrowed.Contains(book.ISBN);
        }

        public void ReturnBook(Book book)
        {
            if (!this.m_Borrowed.Contains(book.ISBN))
            {
                return;
            }

            this.m_Borrowed.Remove(book.ISBN);
        }
    }
}