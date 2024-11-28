namespace LibraryExercise
{
    /// <summary>
    /// ISBN code for tracking books.
    /// </summary>
    public class ISBN
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ISBN" /> class.
        /// </summary>
        /// <param name="value">The value to initialize.</param>
        /// <exception cref="ArgumentException">
        /// When the ISBN code is not 13 characters long.
        /// - or -
        /// When it consists of non-numerical characters.
        /// </exception>
        /// <remarks>For this project's complexity sake ISBN codes are fixed to 13 numerical characters and no checksum will be performed.</remarks>
        public ISBN(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value.Length != 13)
            {
                throw new ArgumentException($"ISBN code is malformed. Expected 13, got {value.Length}");
            }

            if (!IsDigitsOnly(value))
            {
                throw new ArgumentException($"ISBN consists of non numerical characters: {value}");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the value of the ISBN.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc cref="Equals"/>
        public override bool Equals(object? obj)
        {
            return obj is ISBN isbn && isbn.Value.Equals(Value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <inheritdoc cref="GetHashCode"/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Checks if all characters of a string a numeric.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns><c>true</c> when all characters are numerical, <c>false</c> otherwise.</returns>
        private static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }
    }
}