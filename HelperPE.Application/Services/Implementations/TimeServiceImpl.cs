namespace HelperPE.Application.Services.Implementations
{
    public class TimeServiceImpl : ITimeService
    {
        public int GetPairNumber()
        {
            var now = DateTime.Now.TimeOfDay;

            if (now >= new TimeSpan(8, 30, 0) && now <= new TimeSpan(10, 40, 0))
                return 1;
            if (now >= new TimeSpan(10, 41, 0) && now <= new TimeSpan(12, 20, 0))
                return 2;
            if (now >= new TimeSpan(12, 21, 0) && now <= new TimeSpan(14, 30, 0))
                return 3;
            if (now >= new TimeSpan(14, 31, 0) && now <= new TimeSpan(16, 40, 0))
                return 4;
            if (now >= new TimeSpan(16, 41, 0) && now <= new TimeSpan(18, 20, 0))
                return 5;
            if (now >= new TimeSpan(18, 21, 0) && now <= new TimeSpan(20, 10, 0))
                return 6;

            return -1;
        }

        public int GetSemesterNumber()
        {
            var today = DateTime.Today;
            var year = today.Year;

            var firstSemesterStart = new DateTime(year, 9, 1);
            var firstSemesterEnd = new DateTime(year + 1, 2, 1);
            var secondSemesterStart = new DateTime(year, 2, 1);
            var secondSemesterEnd = new DateTime(year, 9, 1);

            if (today >= firstSemesterStart && today < new DateTime(year + 1, 1, 1))
                return 1;

            if (today >= new DateTime(year + 1, 1, 1) && today < firstSemesterEnd)
                return 1;

            return 2;
        }
    }
}
