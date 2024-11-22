using Chaika_TestTask.Helper;

namespace Chaika_TestTask.Core
{
  

    static class DailyPointsCalculator
    {

        /*
         * - В перший день пори року (наприклад September 1 або December 1) користувач отримує 2 поінта.
         * - В другий день пори року користувач отримує 3 поінта. (September 2)
         * В третій та наступний день користувач отримує сумму 100% поінтів за поза 
         * попередній день та 60% поінтів попереднього дня. 
         * Приклад 3-го вересня гравець отримає 60% поінтів із 2-го вересня та 100% поінтів із 1-го вересня.
         */

       /* Сделал кеш на словаре, т.к.баллы не зависят от внешних факторов и нет смысла тратить время каждый раз на подсчёт одних и тех же сумм.
        Считаю, что с текущим ТЗ оптимальнее было привязать кеш не к количеству дней, а к определённой дате, и хранить всего одно значение.
        Ведь для всех пользователей на протяжении 24 часов будет возвращаться одно и то-же значение.
        Но решил пожертвовать каплей ОЗУ, так как посчитал такую реализацию более гибкой.
        Не надо ничего менять, если в дальнейшем баллы будут подсчитываться не от дня в сезоне, а от индивидуальных параметров. К примеру- по количеству дней с активации подписки.*/

        static private Dictionary<int, decimal> cache = new Dictionary<int, decimal>() { { 1, 2 }, { 2, 3 } };

        /* Если я правильно понял, то:
         * Fn = F(n-2) + F(n-1) * 0,6         Условие придумал Фибоначчи?
         * Если нет, то:
         * Fn = F(n-1) + F(n-2) + F(n-1) * 0,6 = F(n-1) * 1,6 + f(n-2)
         */
        static private decimal Calculate(int index)
        {
            if (cache.ContainsKey(index))
                return cache[index];
            decimal result = Calculate(index - 2) + (Calculate(index - 1) * 0.6m);
            cache[index] = result;
            return result;

        }

        public static decimal GetDailyPoints()
        {
            return GetDailyPoints(DateTime.Now);
        }
        public static decimal GetDailyPoints(DateTime date)
        {
            var days = date.DayOfThisSeason();

            return GetDailyPoints(days);
        }
        public static decimal GetDailyPoints(int daysAmount)
        {
            return Math.Ceiling(Calculate(daysAmount));
        }
    }
}
