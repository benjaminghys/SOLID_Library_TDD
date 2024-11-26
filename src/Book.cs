namespace LibraryExercise
{
    /// <summary>
    ///     The book data class.
    /// </summary>
    public class Book
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Book" /> class.
        /// </summary>
        /// <param name="name">The name of the book.</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="isbn">The unique ISBN code of the book.</param>
        /// <exception cref="ArgumentNullException">When isbn is null.</exception>
        public Book(string name, string author, ISBN isbn)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Book name cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author cannot be null or empty.", nameof(author));
            }

            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn), "ISBN cannot be null.");
            Name = name;
            Author = author;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Book" /> class.
        /// </summary>
        /// <param name="name">The name of the book.</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="isbn">The unique ISBN code of the book.</param>
        public Book(string name, string author, string isbn)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Book name cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author cannot be null or empty.", nameof(author));
            }

            Name = name;
            Author = author;
            ISBN = new ISBN(isbn);
        }

        /// <summary>
        ///     Gets the name of the book.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the author of the book.
        /// </summary>
        public string Author { get; }

        /// <summary>
        ///     Gets the unique ISBN code for the book.
        /// </summary>
        public ISBN ISBN { get; }

        /// <inheritdoc cref="GetHashCode" />
        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }
    }
}