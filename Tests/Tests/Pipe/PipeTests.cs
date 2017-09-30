using System;
using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;
using SharpPipe.CurryLib;
using static SharpPipe.Commands;

namespace SharpPipe.Tests
{
    [TestFixture]
    public sealed class PipeTests : TestBase
    {
        [NotNull]
        private static Func<DateTime, DateTime> AddDays( int days ) => d => d.AddDays(days);

        private static PipeFunc<DateTime, string> getshortdate => fun((DateTime d) => d.ToShortDateString());


        private static Pipe<string> YesterdayPipe => start<DateTime>.pipe | DateTime.Now
                                                     | AddDays(-1)
                                                     | getshortdate
                                                     | (i => "Yesterday: " + i);

        [Test]
        public void Test_Pipe_OUT()
            {
                string yesterday = YesterdayPipe | !get;

                string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
                Assert.That(yesterday, Is.EqualTo(expected));
            }

        [Test]
        public void Test_SharpFunc_Invoke()
            {
                DateTime nowDateTime = DateTime.Now;

                DateTime pipe = start<DateTime>.pipe | nowDateTime
                                | when | (i => getshortdate[i] == nowDateTime.ToShortDateString()) | map | (i => nowDateTime)
                                | !get;

                Assert.That(pipe, Is.EqualTo(nowDateTime));
            }

        [Test]
        public void Test_Act()
            {
                Unit pipe = YesterdayPipe
                           | tee | write;

                string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
                Assert.That(WriteOutput, Is.EqualTo(expected));
            }

        [Test]
        public void Test_Path()
            {
                const string projectDirectory = @"C:\Project";
                const string inPath = @"Library\Assembly.dll";

                string combined = start.str.pipe | inPath
                                  | path.prepend(projectDirectory)
                                  | path.getfullpath
                                  | !get;

                Assert.That(combined, Is.EqualTo(Path.Combine(projectDirectory, inPath)));
            }

        [Test]
        public void Test_Or()
            {
                string str = start.str.pipe
                             | (string) null
                             | or | "ALT"
                             | !get;

                Assert.That(str, Is.EqualTo("ALT"));
            }

        [Test]
        public void Test_Bind()
            {
                Func<int, string> toStringFunc = i => i.ToString();

                string str = start.integer.pipe
                             | 10
                             | mapf(toStringFunc)
                             | !get;

                Assert.That(str, Is.EqualTo("10"));
            }
    }
}