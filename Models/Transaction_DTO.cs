using static Chaika_TestTask.Models.Transaction;

namespace Chaika_TestTask.Models
{
    public class Transaction_DTO
    {
        public int Id { get; private set; }
        public TransactionType TransactionTypeProperty { get; private set; }
        public decimal Summ { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public TransactionState State { get; private set; }
        public string? AuthorizedUser { get; private set; }
        public TransactionIcon? Icon { get; private set; }
        public string? RelatedUser { get; private set; }

        public class Builder
        {
            private readonly Transaction _transaction;
            private Transaction_DTO _transaction_DTO;
            public Builder(Transaction transaction) 
            {
                if(transaction==null)
                    throw new ArgumentNullException(nameof(transaction));
                _transaction = transaction;
                _transaction_DTO = new Transaction_DTO
                {
                    Id = _transaction.Id, 
                    AuthorizedUser = _transaction.AuthorizedUser!= null ? _transaction.AuthorizedUser.Name : string.Empty,
                    RelatedUser = _transaction.RelatedUser.Name,
                    Icon = _transaction.Icon,
                    Description = _transaction.Description,
                    CreatedDate = _transaction.CreatedDate,
                    State = _transaction.State,
                    Summ = _transaction.Summ,
                    Name = _transaction.Name,
                    TransactionTypeProperty = _transaction.TransactionTypeProperty
                };

            }
            
            public Transaction_DTO Build()
            {
                
                return _transaction_DTO;
            }



        }
    }
}
