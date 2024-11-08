**Exercise: Design a Simple Library Management System**

### Requirements
You're tasked with building a simple library management system that supports:
1. Adding books to the library.
2. Borrowing books from the library.
3. Returning books to the library.
4. Checking the availability of books.

### Constraints and Rules
1. Each book has a title, author, and unique ISBN.
2. A book can only be borrowed if it is available.
3. When a book is borrowed, it becomes unavailable.
4. When a book is returned, it becomes available again.
5. If a user tries to borrow a book that is not available, they should receive an appropriate message.

---

### Steps to Approach
1. **TDD Workflow**: 
   - Start by writing unit tests for each of the requirements before writing the actual code.
   - Follow the "Red-Green-Refactor" cycle for each feature.
   
2. **SOLID Principles**:
   - **S**: Single Responsibility - Each class should have one reason to change. For instance, you might have separate classes for `Book`, `Library`, and `BorrowingService`.
   - **O**: Open/Closed - Classes should be open for extension but closed for modification. Think about how you might extend functionality without altering existing classes.
   - **L**: Liskov Substitution - Derived classes must be substitutable for their base classes. Ensure your design respects this when using inheritance.
   - **I**: Interface Segregation - Prefer small, focused interfaces over large, general-purpose ones.
   - **D**: Dependency Inversion - Depend on abstractions rather than concrete implementations. Consider using dependency injection where appropriate.

---

### Example Classes to Implement (just for inspiration)
- **Book**: Represents a book with properties like Title, Author, and ISBN.
- **Library**: Manages the collection of books and handles borrowing and returning operations.
- **User**: Represents a user who borrows books (if needed).
- **IBorrowingService**: An interface for the borrowing service, to practice Interface Segregation.

---

Take your time with this exercise, and remember to write your tests first before implementing functionality. 
Good luck, and have fun coding!
