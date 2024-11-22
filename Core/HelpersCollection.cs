namespace Chaika_TestTask.Helper
{
    public static class DecimalExtension
    {

        public static decimal CreateRandomValue(this decimal variable, decimal startvalue = 0, decimal endvalue = 1500)
        {
            Random random = new Random();
            int nextRandomValue = random.Next((int)(startvalue * 100), (int)(endvalue * 100));

            return ((decimal)nextRandomValue) / 100;

        }


        static string[] MoneyFormatsNames = { "", "K", "M", "B", "T", "A", "Aa", "Ab", "Ac", "Ad", "Ae", "Af", "Ag", "Ah" };
        public static string FormatMoney(this decimal amount, int accuracy = 0)
        {
            int n = 0;
            while (Math.Abs(amount) >= 1000)
            {
                amount /= 1000;
                n++;
            }

            // Option 1:
            amount = Math.Round(amount, accuracy);

            // Option 2:
            /*
            decimal RoundedValue = Math.Round(value, Accuracy);
            value = (RoundedValue == (int) RoundedValue ? (int) RoundedValue : RoundedValue);
            */

            if (n < MoneyFormatsNames.Length)
            {
                return string.Format("{0}{1}", amount, MoneyFormatsNames[n]);
            }
            else
            {
                n *= 3;
                while (Math.Abs(amount) >= 10)
                {
                    amount /= 10;
                    n++;
                }
                return string.Format("{0}E+{1}", amount, n);
            }
        }


    }

    public static class DateTimeExtension
    {
        public static Season GetSeason(this DateTime date)
        {
            int month = date.Month;

            if (month >= 3 && month <= 5)
            {
                return Season.Spring;
            }
            else if (month >= 6 && month <= 8)
            {
                return Season.Summer;
            }
            else if (month >= 9 && month <= 11)
            {
                return Season.Autumn;
            }
            else
            {
                return Season.Winter;
            }
        }



        public static int DayOfThisSeason(this DateTime date)
        {
            var season = date.GetSeason();
            if (date.Month > 2)
                return DateTime.Now.DayOfYear - new DateTime(DateTime.Now.Year, (int)season, 1).DayOfYear + 1;
            else
                return DateTime.Now.DayOfYear - new DateTime(DateTime.Now.Year, 1, 1).DayOfYear + 1 + 31;
        }


        public enum Season
        {
            Spring = 3,
            Summer=6,
            Autumn =9,
            Winter=12
        }
    }
}
