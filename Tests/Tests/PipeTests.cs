using System;
using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.Commands.FUNC<System.DateTime>;
using static SharpPipe.Curry.PATH;

namespace SharpPipe.Tests  {
	[TestFixture]
	public sealed class PipeTests : TestBase {
		[NotNull]
		private static Func<DateTime, DateTime> AddDays( int days ) => d => d.AddDays(days);

		private static PipeFunc<DateTime, string> GetShortDate => New(d => d.ToShortDateString());

		
		private static Pipe<string> YesterdayPipe => START.DATETIME.PIPE | DateTime.Now
		                                             | AddDays(-1)
		                                             | GetShortDate
		                                             | (i => "Yesterday: " + i);

		[Test]
		public void Test_Pipe_OUT() {
			var yesterday   = YesterdayPipe | OUT;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(yesterday, Is.EqualTo(expected));
		}
		
		[Test]
		public void Test_SharpFunc_Invoke() {
			var nowDateTime = DateTime.Now;	
			
			var pipe = START.DATETIME.PIPE | nowDateTime
			           | AddDays(-1)
			           | IF | (i => GetShortDate[i] == nowDateTime.ToShortDateString()) | SELECT | (i => nowDateTime)
			           | OUT;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(pipe, Is.EqualTo(nowDateTime));
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

			string combined = START.STRING.PIPE | inPath
			                  | PathPrepend(projectDirectory)
			                  | GetFullPath
			                  | OUT;
			
			Assert.That(combined, Is.EqualTo( Path.Combine(projectDirectory, inPath) ));
		}
	}
}