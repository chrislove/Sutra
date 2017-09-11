using System;
using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;
using SharpPipe.CurryLib;
using static SharpPipe.Commands;

namespace SharpPipe.Tests  {
	[TestFixture]
	public sealed class PipeTests : TestBase {
		[NotNull]
		private static Func<DateTime, DateTime> AddDays( int days ) => d => d.AddDays(days);

		private static PipeFunc<DateTime, string> getshortdate => func.takes<DateTime>.from(d => d.ToShortDateString());

		
		private static Pipe<string> YesterdayPipe => start<DateTime>.pipe | DateTime.Now
		                                             | AddDays(-1)
		                                             | getshortdate
		                                             | (i => "Yesterday: " + i);

		[Test]
		public void Test_Pipe_OUT() {
			var yesterday   = YesterdayPipe | ret;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(yesterday, Is.EqualTo(expected));
		}
		
		[Test]
		public void Test_SharpFunc_Invoke() {
			var nowDateTime = DateTime.Now;	
			
			var pipe = start<DateTime>.pipe | nowDateTime
			           | when | (i => getshortdate[i] == nowDateTime.ToShortDateString()) | select | (i => nowDateTime)
			           | ret;
			
			Assert.That(pipe, Is.EqualTo(nowDateTime));
		}

		[Test]
		public void Test_Act() {
			var pipe = YesterdayPipe
			           | act | write;
			
			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
			Assert.That(WriteOutput, Is.EqualTo(expected) );
		}

		[Test]
		public void Test_Path() {
			const string projectDirectory = @"C:\Project";
			const string inPath = @"Library\Assembly.dll";

			string combined = start.str.pipe | inPath
			                  | path.prepend(projectDirectory)
			                  | path.getfullpath
			                  | ret;
			
			Assert.That(combined, Is.EqualTo( Path.Combine(projectDirectory, inPath) ));
		}
	}
}