using System;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests  {
	[TestFixture]
	public sealed class Tests : TestBase {
		private static SharpFunc<DateTime, DateTime> AddDays( int days ) => _(( DateTime d ) => d.AddDays(days) );

		private static SharpFunc<DateTime, string> GetShortDate => _(( DateTime d ) => d.ToShortDateString());

		private static Pipe<string> YesterdayPipe {
			get {
				return IN(DateTime.Now)
				       | AddDays(-1)
				       | GetShortDate
				       | _(i => "Yesterday: " + i);
			}
		}
		
		[Test]
		public void Test_Pipe_OUT() {
			var yesterday =
				YesterdayPipe
				| OUT;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(yesterday, Is.EqualTo(expected));
		}


		[Test]
		public void Test_SharpAct_DecompositionOperator_Executes() {
			var pipe =
				YesterdayPipe
				| ~WriteLine;

			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(WriteLineOutput, Is.EqualTo(expected) );
		}
		
		[Test]
		public void Test_SharpAct_DO_Executes() {
			var pipe =
				YesterdayPipe
				| WriteLine
				| DO;

			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(WriteLineOutput, Is.EqualTo(expected) );
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