using Chaika_TestTask.Models;
using Chaika_TestTask.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Chaika_TestTask.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StartUpController : ControllerBase
    {
        ITransactionRepository _transactionRepository;
        IUserRepository _userRepository;

        public StartUpController(IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// Creates test data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> PostData()
        {
            for (int i = 0; i < 5; i++)
                _userRepository.InsertUser(new User() { Name = $"User {i + 1}" });
            _userRepository.Save();

            for (int i = 0; i < 5; i++)
                for (int i2 = 0; i2 < 11; i2++)
                {
                    _transactionRepository.InsertTransaction(new Transaction()
                    {
                        Name = GetRandomName(),
                        AuthorizedUser = (i2 % 2 == 0) ? _userRepository.GetUserByID(1) : _userRepository.GetUserByID(2),
                        CreatedDate = DateTime.Now.AddDays(-i).AddHours(-i2),
                        Description = "Description example",
                        RelatedUser = _userRepository.GetUserByID(1),
                        State = (i2 % 3 == 0) ? Transaction.TransactionState.Pending : Transaction.TransactionState.Approved,
                        TransactionTypeProperty = (i2 % 4 == 0) ? Transaction.TransactionType.Payment : Transaction.TransactionType.Credit,
                        Summ = 0,
                        Icon = new TransactionIcon() { BackgroundColor = "DarkGray", Path = "IconName" }
                    }
                    );
                }
            _transactionRepository.Save();
            return StatusCode(200);

        }

        string GetRandomName()
        {
            switch (new Random().Next(0, 5))
            {
                case 0: return "Ikea";
                case 1: return "MacStore";
                case 2: return "Jusk";
                case 3: return "City24";
                default: return "Chaika";
            }

        }
    }
}
