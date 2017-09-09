using System;
using JetBrains.Annotations;
using NUnit.Framework;
using static System.IO.Path;
using static SharpPipe.PathUtil;
using static SharpPipe.Commands;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests  {
	[TestFixture]
	public sealed class PipeTests : TestBase {
		[NotNull]
		private static Func<DateTime, DateTime> AddDays( int days ) => d => d.AddDays(days);

		private static SharpFunc<DateTime, string> GetShortDate => _(( DateTime d ) => d.ToShortDateString());

		
		private static Pipe<string> YesterdayPipe => NEW.DATETIME.PIPE | DateTime.Now
		                                             | AddDays(-1)
		                                             | GetShortDate
		                                             | (i => "Yesterday: " + i);

		[Test]
		public void Test_Pipe_OUT() {
			var yesterday = YesterdayPipe | OUT;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(yesterday, Is.EqualTo(expected));
		}

		[Test]
		public void Test_Act() {
			var pipe = YesterdayPipe
			           | ACT | Write;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(WriteOutput, Is.EqualTo(expected) );
		}

		[Test]
		public void Test_Path() {
			const string projectDirectory = @"C:\Project";
			const string inPath = @"Library\Assembly.dll";

			string combined = NEW.STRING.PIPE | inPath
			                  | PathPrepend(projectDirectory)
			                  | GetFullPath
			                  | OUT;
			
			Assert.That(combined, Is.EqualTo( Combine(projectDirectory, inPath) ));
		}
	}
}