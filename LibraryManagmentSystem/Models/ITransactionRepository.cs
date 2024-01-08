using System.Collections.Generic;

namespace LibraryManagementSystem.Models
{
    public interface ITransactionRepository
    {
        List<TransactionModel> GetAll();

        TransactionModel AddTran(TransactionModel transaction);
        TransactionModel Update(TransactionModel transaction);
        List<TransactionModel> Lended_Books(TransactionModel transaction);
        TransactionModel Return(TransactionModel transaction, int id);
        void Issue_status(int id);
        void Return_Status(int id);
    }
}
