using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chaika_TestTask.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }        
        public TransactionType TransactionTypeProperty { get; set; }
        public decimal Summ { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public TransactionState State { get; set; }
        
        
        public TransactionIcon? Icon { get; set; }

        public required User RelatedUser { get; set; }
        public User? AuthorizedUser { get; set; }


        public enum TransactionType
        {
            Payment,
            Credit
        }
        public enum TransactionState
        {
            Pending,
            Approved
        }
        

    }
}
