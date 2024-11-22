using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chaika_TestTask.Models
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            PrimaryTransactionList = new List<Transaction>();
            DependentTransactionList = new List<Transaction>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Transaction> PrimaryTransactionList { get; set; }
        public List<Transaction> DependentTransactionList { get; set; }

    }
}
