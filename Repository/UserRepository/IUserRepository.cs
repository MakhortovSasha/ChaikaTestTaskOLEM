using Chaika_TestTask.Models;

namespace Chaika_TestTask.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int id);
        void InsertUser(User user);
        void DeleteUser(int id);
        void UpdateUser(User user);
        void Save();
    }
}
