using System;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe {
	[TestFixture]
	public sealed class Tests {
		private static DateTime AddDays(DateTime dateTime, int days ) => dateTime.AddDays(days);
		private static Func<DateTime, DateTime> AddDaysF( int days ) => d => AddDays(d, days);

		private static SharpFunc<DateTime, DateTime> AddDaysSF(int days) => _( AddDaysF(days) );


		private static string GetLongDate( DateTime date ) => date.ToLongDateString();

		private static string GetShortDate(DateTime date) => date.ToShortDateString();

		private static void WriteStuff<T>( T stuff ) => Console.WriteLine(stuff);
		private static void DoStuff( DateTime stuff ) => Console.WriteLine(stuff);

		[Test]
		public void TestGetPipe() {
			var yesterday =
					IN(DateTime.Now)
					| _( AddDaysF(-1) )
					| _<DateTime, string>(GetShortDate)
					| _<string, string>(i => "Yesterday: " + i)
					| __;

			Console.WriteLine(yesterday);
		}
		
		[Test]
		public void TestActPipe() {
			_(
			  IN(DateTime.Now)
			  | _( AddDaysF(+30) )
			  | _<DateTime, string>(GetLongDate)
			  | _<string, string>(i => "Next month: " + i)
			  | _<object>(Console.WriteLine)
			 );
		}
	}
}