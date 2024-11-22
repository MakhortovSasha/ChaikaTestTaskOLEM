using Chaika_TestTask.Core.Database;
using Chaika_TestTask.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Chaika_TestTask.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private AppDBContext _dbContext;
        public TransactionRepository(AppDBContext context) 
        {
            _dbContext = context;
        }
        public void DeleteTransaction(int id)
        {
            if (_dbContext.Transactions == null) 
                return;
            Transaction? toDelete = _dbContext.Transactions.FirstOrDefault(t => t.Id == id);
            if (toDelete != null) 
            _dbContext.Transactions.Remove(toDelete);
        }

       

        public Transaction GetTransactionByID(int id)
        {
            if (_dbContext.Transactions == null)
                 throw new NullReferenceException();
            var returnable = _dbContext.Transactions.Find(id);

            _dbContext.Entry(returnable).Reference(r => r.RelatedUser).Load();
                _dbContext.Entry(returnable).Reference(r => r.AuthorizedUser).Load();
                _dbContext.Entry(returnable).Reference(r => r.Icon).Load();
            return returnable;
        }

        public IEnumerable<Transaction> GetTransactions(
            Expression<Func<Transaction, bool>>? filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>? orderBy = null,
            string includeProperties = "")
        {
            if (_dbContext.Transactions == null)
                throw new NullReferenceException();
            IQueryable<Transaction> query = _dbContext.Transactions;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            var returnable = new List<Transaction>();
            if (orderBy != null)
            {
                returnable = orderBy(query).ToList();
            }
            else
            {
                returnable = query.ToList();
            }
            foreach (var e in returnable.Where(t => t.RelatedUser == null))
                _dbContext.Entry(e).Reference(r => r.RelatedUser).Load();
            foreach (var e in returnable.Where(t => t.AuthorizedUser == null))
                _dbContext.Entry(e).Reference(r => r.AuthorizedUser).Load();
            foreach (var e in returnable.Where(t => t.Icon == null))
                _dbContext.Entry(e).Reference(r => r.Icon).Load();
            return returnable;
        }

        

        public void InsertTransaction(Transaction transaction)
        {
            if (_dbContext.Transactions == null)
                return;
            _dbContext.Transactions.Add(transaction);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _dbContext.Entry(transaction).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
