using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Models
{
    public class TransactionRepository : ITransactionRepository
    {
        ProjectDbContext _projectDbContext;
        IBookRepository _bookRepository;
        public TransactionRepository(ProjectDbContext projectDbContext, IBookRepository bookRepository)
        {

            _projectDbContext = projectDbContext;
            _bookRepository = bookRepository;

        }

        //-----------------------------------------------------------------------------
        public TransactionModel AddTran(TransactionModel transaction)
        {
            _projectDbContext.TransactionsTable.Add(transaction);

            transaction.issue_date = DateTime.Now;
            transaction.expected_return_date = DateTime.Now.AddDays(7);
            /*transaction.return_date = DateTime.Now.AddDays(7);
            transaction.past_due_day = */
            _projectDbContext.SaveChanges();
            return transaction;
        }
        public TransactionModel Update(TransactionModel transaction)
        {
            _projectDbContext.Attach(transaction);
            _projectDbContext.Entry(transaction).State = EntityState.Modified;
            _projectDbContext.SaveChanges();
            return transaction;
        }
        //----------------------------------------------------------------------------

        public List<TransactionModel> GetAll()
        {
            return _projectDbContext.TransactionsTable.ToList();

        }

        public List<TransactionModel> Lended_Books(TransactionModel transaction)
        {
            BooksModel books = new BooksModel();
            books = _projectDbContext.BooksTable.Find(transaction.book_id);

            transaction.book_id = books.book_id;
            transaction.book_title = books.title;
            transaction.user_name = transaction.user_name;
            transaction.issue_status = "Pending";
            //transaction.issue_date = DateTime.Now;
            //transaction.expected_return_date = DateTime.Now.AddDays(7);

            _projectDbContext.TransactionsTable.Add(transaction);
            _projectDbContext.SaveChanges();
            return _projectDbContext.TransactionsTable.Select(o => o).ToList();
        }

        public TransactionModel Return(TransactionModel transaction, int id)
        {
            TransactionModel transactionModel = _projectDbContext.TransactionsTable.Find(id);
            //transactionModel.return_date = DateTime.Now;
            transactionModel.return_status = "Pending";

            _projectDbContext.SaveChanges();
            return transactionModel;
        }

        public void Issue_status(int id)
        {
            TransactionModel transactionModel = _projectDbContext.TransactionsTable.Find(id);
            transactionModel.issue_date = DateTime.Now;
            transactionModel.expected_return_date = DateTime.Now.AddDays(7);
            transactionModel.issue_status = "Approved";

            _bookRepository.Lend_Book(transactionModel.book_id);//for update in the book table

            _projectDbContext.SaveChanges();
        }

        public void Return_Status(int id)
        {
            TransactionModel transactionModel = _projectDbContext.TransactionsTable.Find(id);
            transactionModel.return_date = DateTime.Now;
            transactionModel.return_status = "Approved";

            _bookRepository.Return_Book(transactionModel.book_id);//for update in the book table

            _projectDbContext.SaveChanges();
        }
    }
}
