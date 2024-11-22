using System.Text.Json.Serialization;

namespace Chaika_TestTask.Models
{
    public class TransactionIcon
    {
        public int ID { get; set; }
        public string ?BackgroundColor { get; set; }
        public string ?Path { get; set; }
        [JsonIgnore]
        public List<Transaction>? Transactions { get; set; }
    }
}
