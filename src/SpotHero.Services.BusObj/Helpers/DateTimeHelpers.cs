using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Helpers
{
    public static class DateTimeHelpers
    {
        //src: https://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
        public static DateTime GetDateTimeNextWeekday(this DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
