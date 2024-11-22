using Chaika_TestTask.Models;
using Chaika_TestTask.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Transaction = Chaika_TestTask.Models.Transaction;

namespace Chaika_TestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {

        ITransactionRepository _transactionRepository;
        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;            
        }


        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public ActionResult<Transaction_DTO> GetTransactionDetails(int id)
        {
            var transaction = _transactionRepository.GetTransactionByID(id);
            if (transaction != null)
            {
                var result = new Transaction_DTO.Builder(transaction).Build();
               
                    return result;
            }

            return NotFound();
        }

        // GET: api/Transactions/GetDetailsByUser/5
        [HttpGet("GetDetailsByUser/{id}")]
        public ActionResult<TransactionsListScreen_DTO> GetTransactionsScreenData(int id)
        {

            var builder = new TransactionsListScreen_DTO.Builder(id, _transactionRepository);
            builder.AddCardLimit();
            builder.AddCardBalance();
            builder.AddNoPaymentDue_Message();
            builder.AddDailyPoints();
            builder.AddLastTenTransactions();

            var transactionLS = builder.Build();
            if (transactionLS != null)
                return transactionLS;

            return NotFound();
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            _transactionRepository.DeleteTransaction(id);
            _transactionRepository.Save();
            return NotFound();
        }
    }
}
