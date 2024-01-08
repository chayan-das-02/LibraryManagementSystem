using System.Collections.Generic;

namespace LibraryManagementSystem.Models
{
    public interface IBookRepository
    {
        IEnumerable<BooksModel> GetAllBooks();

        BooksModel Add_Book(BooksModel book);

        BooksModel Delete_Book(BooksModel book);

        BooksModel GetBookById(int id);

        void Lend_Book(int bookId);

        void Return_Book(int bookId);

        BooksModel Edit_Book(BooksModel book, int id);
    }
}
