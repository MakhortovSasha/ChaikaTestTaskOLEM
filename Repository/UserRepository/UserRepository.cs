using Chaika_TestTask.Core.Database;
using Chaika_TestTask.Models;
using System.Data.Entity;

namespace Chaika_TestTask.Repository
{
    public class UserRepository : IUserRepository
    {

        private AppDBContext context;

        public UserRepository(AppDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            if (context.Users == null)
               throw new NullReferenceException();
            var returnable = context.Users.AsEnumerable();

            foreach (var e in returnable.Where(t => t.PrimaryTransactionList == null))
                context.Entry(e).Reference(r => r.PrimaryTransactionList).Load();
            foreach (var e in returnable.Where(t => t.DependentTransactionList == null))
                context.Entry(e).Reference(r => r.DependentTransactionList).Load();
            return returnable;
        }

        public User GetUserByID(int id)
        {
            if (context.Users == null)
                throw new NullReferenceException();
            var returnable = context.Users.Find(id);

            context.Entry(returnable).Reference(r => r.PrimaryTransactionList).Load();
            context.Entry(returnable).Reference(r => r.DependentTransactionList).Load();
            return returnable;
        }

        public void InsertUser(User user)
        {
            if (context.Users == null)
                return;
            context.Users.Add(user);
        }

        public void DeleteUser(int userID)
        {
            if (context.Users == null)
                return;
            User user = context.Users.Find(userID);
            if (user != null)
            {
                context.Users.Remove(user);
            }
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

       

    }
}
