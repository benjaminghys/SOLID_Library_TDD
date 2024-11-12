namespace LibraryExercise
{
    /// <summary>
    /// ISBN code for tracking books.
    /// </summary>
    public class ISBN
    {
        /// <summary>
        /// Gets the value of the ISBN.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ISBN"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentException">When the ISBN code is not 13 characters long or when it consists of non-numerical characters.</exception>
        /// <remarks>For complexity’s sake ISBN are fixed to 13 numerical characters and no checksum will be performed.</remarks>
        public ISBN(string value)
        {
            if (value.Length != 13)
            {
                throw new ArgumentException($"ISBN code is malformed. Expected 13, got {value.Length}");
            }

            if (!IsDigitsOnly(value))
            {
                throw new ArgumentException($"ISBN consists of non numerical characters: {value}");
            }

            this.Value = value;
        }

        /// <inheritdoc cref="Equals"/>>
        public override bool Equals(object? obj)
        {
            return obj is ISBN isbn && isbn.Value.Equals(this.Value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <inheritdoc cref="GetHashCode"/>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Checks if all characters of a string a numeric.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>True when all characters are numerical. False otherwise.</returns>
        private static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }
    }
}