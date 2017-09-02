using System;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests  {
	[TestFixture]
	public sealed class Tests : TestBase {
		private static SharpFunc<DateTime, DateTime> AddDays( int days ) => _(( DateTime d ) => d.AddDays(days) );

		private static SharpFunc<DateTime, string> GetLongDate  => _(( DateTime d ) => d.ToLongDateString());
		private static SharpFunc<DateTime, string> GetShortDate => _(( DateTime d ) => d.ToShortDateString());

		[Test]
		public void TestGetPipe() {
			var yesterday =
				IN(DateTime.Now)
				| AddDays(-1)
				| GetShortDate
				| _(i => "Yesterday: " + i)
				| OUT;

			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();

			Assert.That(yesterday, Is.EqualTo(expected));
		}

		[Test]
		public void TestActPipe() {
			// Arrange

			// Act
			var pipe =
				IN(DateTime.Now)
				| AddDays(+30)
				| GetLongDate
				| _(i => "Next month: " + i)
				| ~WriteLine;

			// Assert
			string expectedOutput = "Next month: " + DateTime.Now.AddDays(+30).ToLongDateString();
			
			Assert.That(WriteLineOutput, Is.EqualTo(expectedOutput) );
		}


		[Test]
		public void TestFunctionComposition() {
			var add10Func = _(( int i ) => i + 10);
			var mult5Func = _(( int i ) => i * 5);

			int pipe = IN(2)
			           | add10Func + mult5Func
			           | OUT;

			Assert.That(pipe, Is.EqualTo(60));
		}
	}
}