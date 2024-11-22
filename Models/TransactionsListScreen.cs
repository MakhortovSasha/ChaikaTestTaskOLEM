using Chaika_TestTask.Core;
using Chaika_TestTask.Helper;
using Chaika_TestTask.Repository;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;

namespace Chaika_TestTask.Models
{
    public class TransactionsListScreen_DTO
    {

        

        public  decimal CardBalance { get; set; }
        public decimal CardLimit { get; set; }
        public decimal AvailableMoney { get; set; }
        public  string? NoPaymentDue_Message { get; set; }
        public  long DailyPoints { get; set; }
        
        public IEnumerable<Transaction_DTO>? LatestTransactions { get; set; } // = new Transaction[10];


        public class Builder
        {
            private readonly ITransactionRepository tRepository;
            private readonly int _userid;
            private TransactionsListScreen_DTO _data;
            public Builder(int userid, ITransactionRepository transactionRepository)
            {
                tRepository = transactionRepository;
                _userid = userid;
                _data = new TransactionsListScreen_DTO();
            }

            /*Блок Card Balance – максимальний ліміт картки $1500. 
             * Card Balance – випадкове число.
             * Available це ліміт мінус balance.
             */

            
            public void AddCardBalance()
            {                
                _data.CardBalance = _data.CardBalance.CreateRandomValue();
            }



            /*Лимит так и просится, чтоб его вытягивали из базы, но в рамках тестового задания не было прописано его хранение в базе и что он из себя представляет: 
             * Параметр карты;
             * Параметр пользователя;
             * Глобальную конфигурацию.

             * Оставил как в ТЗ: фиксированным значением. "Потрібно створити простий бекенд"
             */
            public void AddCardLimit(decimal amount = 1500)
            {

                _data.CardLimit = amount;

            }

            /*Кількість отриманих поінтів 
            *округлюється(якщо поінтів більше ніж 1000, то виводиться в форматі 1K.
            * Наприклад 28745 поінтів виводиться як 29K).
            */
            public void AddDailyPoints()
            {
                _data.DailyPoints = (long)DailyPointsCalculator.GetDailyPoints();
                //Решил, что округлять до целых вполне нормально для бека, а до тысяч-задача фронта.
                //Но сделал метод расширение для округления на беке
                DailyPointsCalculator.GetDailyPoints().FormatMoney();
                //Не стал мучить вопросами рекрутёра- не логично менять бек, если появится кнопка "отображать полностью количество баллов" на UI

            }
            public void AddNoPaymentDue_Message()
            {
                //. Блок No Payment Due – має виводитись напис You’ve paid your <Поточний місяць> balance.
                _data.NoPaymentDue_Message = $"You’ve paid your {DateTime.Now.ToString("MMMM", new CultureInfo("en-GB"))} balance.";
            }

            public void AddLastTenTransactions()
            {

                var result = tRepository.GetTransactions().Where(t => t.RelatedUser.Id == _userid).OrderBy(a => a.CreatedDate).Take(10).Select(x => new Transaction_DTO.Builder(x).Build()); ;
               


                _data.LatestTransactions = result;
            }


            public TransactionsListScreen_DTO Build()
            {
                return _data;
            }
        }

    }
}

