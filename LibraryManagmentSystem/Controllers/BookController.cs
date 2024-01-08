using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        IBookRepository _bookRepository;
        IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._bookRepository = bookRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Books()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        /*[Authorize(policy: HttpContext.Session.GetString = "Admin")]*/
        public IActionResult AllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public IActionResult AddBooks()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBooks(BooksModel books)
        {
            //var addBooks = _bookRepository.Equals;
            /*books = _bookRepository.Add_Book(books);
            TempData["successMessage"] = "Book Added";
            return RedirectToAction("AllBooks");*/
            //===================================//

            BooksModel booksModel = new BooksModel();
            //save img to wwwroot/images
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imgName = Path.GetFileName(books.ImgFile.FileName);

            books.image_path = imgName;
            string path = Path.Combine(wwwRootPath + "/images", imgName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await books.ImgFile.CopyToAsync(fileStream);
            }
            _bookRepository.Add_Book(books);
            TempData["successMessage"] = "Book Added in the Database!!!";

            return RedirectToAction("AllBooks");
        }

        [HttpGet]
        public IActionResult RemoveBooks(int id)
        {
            BooksModel book =_bookRepository.GetBookById(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult RemoveBooks(BooksModel book)
        {
            if(book.lended_unit < 0)
            {
                book = _bookRepository.Delete_Book(book);
                TempData["successMessage"] = "Book Deleted"+book.lended_unit;
                return RedirectToAction("AllBooks");
            }
            else
            {
                TempData["successMessage"] = "You can not delete the book.";
                return RedirectToAction("AllBooks");
            }
        }

        
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);
            HttpContext.Session.SetInt32("bookId", book.book_id);
            HttpContext.Session.SetString("bookTitle", book.title);
            return View(book);

        }

        [HttpGet]
        public IActionResult EditBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(BooksModel books, int id)
        {
            //save img to wwwroot/images
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imgName = Path.GetFileName(books.ImgFile.FileName);

            books.image_path = imgName;
            string path = Path.Combine(wwwRootPath + "/images", imgName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await books.ImgFile.CopyToAsync(fileStream);
            }
            _bookRepository.Edit_Book(books, id);
            TempData["successMessage"] = "The book has been updated!!!";

            return RedirectToAction("AllBooks");
        }
    }
}
