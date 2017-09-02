﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
	[TestFixture]
	public sealed class Tests {
		private static SharpFunc<DateTime, DateTime> AddDays( int days ) => _(( DateTime d ) => d.AddDays(days) );

		private static SharpFunc<DateTime, string> GetLongDate => _(( DateTime d ) => d.ToLongDateString());
		private static SharpFunc<DateTime, string> GetShortDate => _(( DateTime d ) => d.ToShortDateString());

		[Test]
		public void TestGetPipe() {
			var yesterday =
				IN(DateTime.Now)
				| AddDays(-1)
				| GetShortDate
				| _(i => "Yesterday: " + i)
				| ___;

			string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();

			Assert.That(yesterday, Is.EqualTo(expected));
		}

		[Test]
		public void TestActPipe() {
			_(
			  IN(DateTime.Now)
			  | AddDays(+30)
			  | GetLongDate
			  | _(i => "Next month: " + i)
			  | WriteLine
			 );
		}

		[Test]
		public void TestEnumerablePipeAction() {
			_(
			  IN(Enumerable.Range(0, 10)) + Enumerable.Range(10, 10)
			  | WriteLine
			 );
		}

		[Test]
		public void TestEnumerablePipeComposition() {
			Func<string, IEnumerable<string>> getEnumFuncA = i => Enumerable.Repeat(i, 2);
			Func<string, IEnumerable<string>> getEnumFuncB = i => Enumerable.Repeat(i, 3);

			var enumPipeStr = ENUM("")
			                  + getEnumFuncA("A")
			                  + getEnumFuncB("B")
			                  | CONCAT("");

			Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
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