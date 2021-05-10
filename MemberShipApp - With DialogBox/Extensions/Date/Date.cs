
using System;

using System.ComponentModel;


namespace MemberShipApp.Extensions
{
	[TypeConverter(typeof(DateConverter))]
	public struct Date
	{
		private DateTime dtValue;

		public Date(int year, int month, int day)
		{
			dtValue = new DateTime(year, month, day);
		}

		public Date(DateTime dateTime)
		{
			dtValue = dateTime.Date;
		}

		public Date(DateTimeOffset dateTimeOffset)
		{
			dtValue = dateTimeOffset.Date;
		}

		public override string ToString()
		{
			return dtValue.ToString("yyyy-MM-dd");
		}

		public string ToString(string format)
		{
			return dtValue.ToString(format);
		}

		public Date AddDays(int days)
		{
			return new Date(dtValue.AddDays(days));
		}

		public Date AddMonths(int months)
		{
			return new Date(dtValue.AddMonths(months));
		}

		public DateTime Value
		{
			get
			{
				return dtValue;
			}

			private set
			{
				value = value.Date;
			}
		}

		public int Year
		{
			get { return dtValue.Year; }
		}

		public int Month
		{
			get { return dtValue.Month; }
		}

		public int Day
		{
			get { return dtValue.Day; }
		}

		public DayOfWeek DayOfWeek
		{
			get { return dtValue.DayOfWeek; }
		}

		public long Ticks
		{
			get { return dtValue.Ticks; }
		}

		public static implicit operator DateTime(Date date)
		{
			return date.Value;
		}

		public static bool TryParse(string input, out Date date)
		{

			if (DateTime.TryParse(input, out DateTime result))
			{
				date = new Date(result.Year, result.Month, result.Day);

				return true;
			}

			date = new Date(1, 1, 1);

			return false;
		}
	}
}
