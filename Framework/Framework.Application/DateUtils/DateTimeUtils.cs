using System;
using System.Globalization;

namespace Framework.Application.DateUtils;


public static class DateTimeUtils
{

    public static DateTime GetCurrentUtc()
    {
        return DateTime.UtcNow;
    }

    public static DateTime ConvertUtcToLocal(DateTime utcTime)
    {
        return utcTime.ToLocalTime();
    }

    public static DateTime ConvertLocalToUtc(DateTime localTime)
    {
        return localTime.ToUniversalTime();
    }

    public static TimeSpan GetDateDifference(DateTime startDate, DateTime endDate)
    {
        return endDate - startDate;
    }

    public static bool IsPastDate(DateTime date)
    {
        return date < DateTime.UtcNow;
    }

    public static bool IsFutureDate(DateTime date)
    {
        return date > DateTime.UtcNow;
    }

    public static string FormatDateTime(DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return date.ToString(format);
    }

    public static DateTime? ParseDateTime(string dateString, string format = "yyyy-MM-dd HH:mm:ss")
    {
        if (DateTime.TryParseExact(dateString, format, null, DateTimeStyles.None, out var result))
        {
            return result;
        }
        return null;
    }

    public static DateTime GetStartOfDay(DateTime date)
    {
        return date.Date;
    }

    public static DateTime GetEndOfDay(DateTime date)
    {
        return date.Date.AddDays(1).AddTicks(-1);
    }

    public static DateTime AddDays(DateTime date, int days)
    {
        return date.AddDays(days);
    }

    public static DateTime SubtractDays(DateTime date, int days)
    {
        return date.AddDays(-days);
    }

    public static string ConvertToPersianDate(DateTime date)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date):00}/{persianCalendar.GetDayOfMonth(date):00}";
    }

    public static DateTime? ConvertToGregorianDate(string persianDate)
    {
        try
        {
            string[] parts = persianDate.Split('/');
            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int year) &&
                int.TryParse(parts[1], out int month) &&
                int.TryParse(parts[2], out int day))
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
}
