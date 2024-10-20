using System;

namespace LuzyceApi.Core.Dictionaries;

public static class ActualTime
{
    public static DateTime ConvertToEuropeWarsaw(this DateTime date)
    {
        return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
    }
}