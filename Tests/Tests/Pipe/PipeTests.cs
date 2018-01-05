using System;
using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;
using Sutra.CurryLib;
using static Sutra.Commands;

namespace Sutra.Tests
{
    [TestFixture]
    public sealed class PipeTests : TestBase
    {
        [NotNull]
        private static Func<DateTime, DateTime> AddDays( int days ) => d => d.AddDays(days);

        private static PipeFunc<DateTime, string> getShortDate => fun((DateTime d) => d.ToShortDateString());


        private static Pipe<string> YesterdayPipe => start.pipe | DateTime.Now
                                                     | AddDays(-1)
                                                     | getShortDate
                                                     | (i => "Yesterday: " + i);

        [Test]
        public void Test_Get()
            {
                string yesterday = YesterdayPipe | !get;

                string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
                Assert.That(yesterday, Is.EqualTo(expected));
            }

        [Test]
        public void Test_SharpFunc_Invoke()
            {
                DateTime nowDateTime = DateTime.Now;

                DateTime pipe = start.pipe | nowDateTime
                                | when | (i => getShortDate[i] == nowDateTime.ToShortDateString()) | map | (i => nowDateTime)
                                | !get;

                Assert.That(pipe, Is.EqualTo(nowDateTime));
            }

        [Test]
        public void Test_Act()
            {
                var pipe = YesterdayPipe
                           | tee | write;

                string expected = "Yesterday: " + DateTime.Now.AddDays(-1).ToShortDateString();
                Assert.That(WriteOutput, Is.EqualTo(expected));
            }

        [Test]
        public void Test_Path()
            {
                const string projectDirectory = @"C:\Project";
                const string inPath = @"Library\Assembly.dll";

                string combined = start.pipe | inPath
                                  | pathf.prepend(projectDirectory)
                                  | !get;

                Assert.That(combined, Is.EqualTo(Path.Combine(projectDirectory, inPath)));
            }

        [Test]
        public void Test_Or()
            {
                string str = start.pipe
                             | (string) null
                             | or("ALT")
                             | !get;

                Assert.That(str, Is.EqualTo("ALT"));
            }

        [Test]
        public void Test_Mapf()
            {
                Func<int, string> toStringFunc = i => i.ToString();

                string str = start.pipe
                             | 10
                             | mapf(toStringFunc)
                             | !get;

                Assert.That(str, Is.EqualTo("10"));
            }

        [Test]
        public void Test_Where()
            {
                str str = start.pipe
                          | "TEST"
                          | where | (i => i == "ABC")
                          | get;
                
                Assert.That(!str.HasValue);
            }
        
        [Test]
        public void Test_EmptyString_Is_EmptyPipe()
            {
                str option = start.pipe
                             | ""
                             | get;
                
                Assert.That(!option.HasValue);
            }
    
        [Test]
        public void Test_Put()
            {
                int value = start.pipe
                            | "A"
                            | put(0)
                            | !get;
                
                Assert.That(value, Is.EqualTo(0));
            }
    }
}