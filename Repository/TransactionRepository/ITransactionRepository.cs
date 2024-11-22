using Chaika_TestTask.Models;
using System.Linq.Expressions;

namespace Chaika_TestTask.Repository
{
    public interface ITransactionRepository
    {

        IEnumerable<Transaction> GetTransactions(Expression<Func<Transaction, bool>>? filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>? orderBy = null,
            string includeProperties = "");
        Transaction GetTransactionByID(int id);
        void InsertTransaction(Transaction transaction);
        void DeleteTransaction(int id);
        void UpdateTransaction(Transaction transaction);
        void Save();
    }
}
