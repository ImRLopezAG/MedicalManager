namespace MedicalManager.Core.Application.Extensions;

public static class TimeExtension {
  public static TimeOnly ConvertToTime(this DateTime time) => new(time.Hour, time.Minute, time.Second);

  public static DateOnly ConvertToDate(this DateTime date) => new(date.Year, date.Month, date.Day);

}
