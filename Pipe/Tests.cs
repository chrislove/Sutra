using System;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe {
	[TestFixture]
	public sealed class Tests {
		private static DateTime AddDays(DateTime dateTime, int days ) => dateTime.AddDays(days);

		private static string GetLongDate( DateTime date ) => date.ToLongDateString();

		private static string GetShortDate(DateTime date) => date.ToShortDateString();

		private static void WriteStuff<T>( T stuff ) => Console.WriteLine(stuff);
		private static void DoStuff( DateTime stuff ) => Console.WriteLine(stuff);



		[Test]
		public void TestGetPipe() {
			var yesterday = PIPE
			                | DateTime.Now
			                | _<DateTime> ( d => d.AddDays(-1))
			                | _<DateTime> (GetShortDate)
			                | STR;

			Console.WriteLine("Yesterday " + yesterday);
		}
		
		[Test]
		public void TestActPipe() {
			_(PIPE
			  | DateTime.Now
			  | _<DateTime> (d => d.AddDays(10))
			  | _<DateTime> (d => d.ToLongDateString())
			  | _<string>   (s => "Later date: " + s)
			  | __<string>  (Console.WriteLine)
			 );
		}
	}
}