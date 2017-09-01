using System;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe {
	[TestFixture]
	public sealed class Tests {
		private static DateTime AddDays( DateTime dateTime, int days ) => dateTime.AddDays(days);
		private static SharpFunc<DateTime, DateTime> AddDaysF( int days ) => _(( DateTime d ) => AddDays(d, days));

		private static string GetLongDate( DateTime date ) => date.ToLongDateString();

		private static string GetShortDate( DateTime date ) => date.ToShortDateString();

		private static void WriteStuff<T>( T stuff ) => Console.WriteLine(stuff);
		private static void DoStuff( DateTime stuff ) => Console.WriteLine(stuff);

		[Test]
		public void TestGetPipe() {
			var yesterday =
				IN(DateTime.Now)
				| AddDaysF(-1)
				| _(( DateTime d ) => GetShortDate(d))
				//| _<DateTime, string>( GetShortDate )
				| _(i => "Yesterday: " + i)
				| ___;

			Console.WriteLine(yesterday);
		}

		[Test]
		public void TestActPipe() {
			_(
			  IN(DateTime.Now)
			  | AddDaysF(+30)
			  | _(( DateTime d ) => GetLongDate(d))
			  | _(i => "Next month: " + i)
			  | __<string>(Console.WriteLine)
			 );
		}

		[Test]
		public void TestEnumerablePipe() {
			_(
			  IN(Enumerable.Range(0, 10)) + Enumerable.Range(10, 10)
			  | __<int>(Console.WriteLine)
			 );
		}

		[Test]
		public void TestFunctionComposition() {
			var add10Func = _(( int i ) => i + 10);
			var mult5Func = _(( int i ) => i * 5);

			int pipe = IN(2)
			           | add10Func + mult5Func
			           | ___;

			Assert.That(pipe, Is.EqualTo(60));
		}
	}
}