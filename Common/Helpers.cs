namespace Common
{
    public static class Helpers
    {

        public static PartOfDay GetTimeOftheDay(TimeSpan timeSpan)
        {
            if (timeSpan.Hours >= 5 && timeSpan.Hours < 12)
                return PartOfDay.Morning;
            else if (timeSpan.Hours >= 12 && timeSpan.Hours < 17)
                return PartOfDay.Afternoon;
            else if (timeSpan.Hours >= 17 && timeSpan.Hours < 21)
                return PartOfDay.Evening;
            else
                return PartOfDay.Night;
        }

        public static int GetMedianAmt(IEnumerable<int> cus)
        {
            if (!cus.Any())
                return 0;

            var count = cus.Count();

            if (count / 2 != 0 && count % 2 == 0) //even
                return ((cus.ElementAt((count / 2) - 1)) + (cus.ElementAt(count / 2))) / 2;
            else if (count / 2 != 0 && count % 2 != 0) //odd 
                return (cus.OrderBy(ord => ord).ElementAt((count / 2)));
            else // for one element 
                return cus.ElementAt(0);
        }

        public static string GetSeason(DateTime date)
        {
            int month = date.Month;
            return month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public enum PartOfDay
        {
            Morning,
            Afternoon,
            Evening,
            Night
        }
    }
}