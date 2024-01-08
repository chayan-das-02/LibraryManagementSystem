using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManagementSystem.Controllers
{
    public class TransactionController : Controller
    {
        ITransactionRepository _transactionRepository;
        ProjectDbContext _projectDbContext;
        public TransactionController(ITransactionRepository transactionRepository,ProjectDbContext projectDbContext) 
        {
            _transactionRepository = transactionRepository;
            _projectDbContext = projectDbContext;
        }
        public IActionResult Index()
        {
            List<TransactionModel> tranList = _transactionRepository.GetAll();
            return View(tranList);
        }

        public IActionResult LendedBooks()
        {
            List<TransactionModel> tranList = _transactionRepository.GetAll();
            return View(tranList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TransactionModel transactionModel)
        {
            transactionModel = _transactionRepository.AddTran(transactionModel);
            return RedirectToAction("LendedBooks");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TransactionModel transaction = _projectDbContext.TransactionsTable.Find(id);
            return View(transaction);
        }
        [HttpPatch]
        public IActionResult Edit(TransactionModel transaction)
        {
            transaction=_transactionRepository.Update(transaction);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Lend(int id)
        {
            TransactionModel transaction = new TransactionModel();
            BooksModel books = _projectDbContext.BooksTable.Find(id);

            if (books != null && books.quantity > 0)
            {
                transaction.book_id = id;
                transaction.book_title = HttpContext.Session.GetString("bookTitle");
                transaction.user_name = HttpContext.Session.GetString("user_name");
                List<TransactionModel> transactions = _transactionRepository.Lended_Books(transaction);
                TempData["LendMessage"] = "Book has been lended(*Required Librarian's approval)";
                View(transactions);
                return RedirectToAction("LendedBooks");
            }
            else
            {
                TempData["LendMessage"] = "You can not lend this book";
                return RedirectToAction("LendedBooks");
            }
        }

        [HttpGet]
        public IActionResult Return(TransactionModel transaction, int id)
        {
            _transactionRepository.Return(transaction, id);
            return RedirectToAction("LendedBooks");
        }

        [HttpGet]
        public IActionResult Approve_issue(int id)
        {
            _transactionRepository.Issue_status(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Approve_return(int id)
        {
            _transactionRepository.Return_Status(id);
            return RedirectToAction("Index");
        }
    }
}
