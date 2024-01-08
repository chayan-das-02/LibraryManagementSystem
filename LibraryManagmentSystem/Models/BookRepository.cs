
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Models
{
    public class BookRepository : IBookRepository
    {
        ProjectDbContext _bookDbContext;
        public BookRepository(ProjectDbContext bookDbContext)
        {
            this._bookDbContext = bookDbContext;
        }

        public BooksModel Add_Book(BooksModel book)
        {
            _bookDbContext.BooksTable.Add(book);
            if (book.quantity > 0)
            {
                book.status_id = 1;
            }
            else
            {
                book.status_id = 2;
            }

            _bookDbContext.SaveChanges();
            return book;
        }

        public BooksModel Delete_Book(BooksModel book)
        {
            _bookDbContext.Attach(book);
            _bookDbContext.Entry(book).State = EntityState.Deleted;
            _bookDbContext.SaveChanges();
            return book;
            
            
            
            
            /* var data = _bookDbContext.BooksTable.Find(id);
            //_bookDbContext.SaveChanges();
            //return null;

            *//*_bookDbContext.Attach(book);
            _bookDbContext.Entry(book).State = EntityState.Deleted;
            _bookDbContext.SaveChanges();
            return null;*//*
            if (data != null)
            {

                _bookDbContext.BooksTable.Remove(data);
                _bookDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }*/
        }

        public BooksModel GetBookById(int id)
        {
            BooksModel book = _bookDbContext.BooksTable.Where(e => e.book_id == id).FirstOrDefault();
            return book;
        }

        public IEnumerable<BooksModel> GetAllBooks()
        {
            return _bookDbContext.BooksTable.Select(o => o).ToList();
        }

        public void Lend_Book(int bookId)
        {
            var lend = _bookDbContext.BooksTable.Find(bookId);

            lend.quantity -= 1;
            lend.lended_unit += 1;
        }

        public void Return_Book(int bookId)
        {
            var lend = _bookDbContext.BooksTable.Find(bookId);

            lend.quantity += 1;
            lend.lended_unit -= 1;
        }

        public BooksModel Edit_Book(BooksModel book, int id)
        {
            var editBook = _bookDbContext.BooksTable.Find(id);

            editBook.title = book.title;
            editBook.author = book.author;
            editBook.description = book.description;
            editBook.quantity = book.quantity;

            return null;
        }
    }
}
