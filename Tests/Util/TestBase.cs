using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using JetBrains.Annotations;
using NUnit.Framework;
using static Sutra.Commands;
using static Sutra.Conditions;

namespace Sutra.Tests {
    [TestFixture]
    public abstract class TestBase {
        [NotNull] protected readonly string[] ABCArray = {"A", "B", "C"};
        [NotNull] protected List<string>      ABCList  => ABCArray.ToList();
        
        protected Seq<string>                 ABCSeq      => start.seq  | ABCArray;
        protected Pipe<string>                TestPipe    => start.pipe | "TEST";

        protected static Seq<string> EmptyAndTestSeq => start.seq.of<string>()
                                                        | append | (string) null
                                                        | fail| when | isempty
                                                        | append | "TEST";
        
        protected string WriteOutput;

        [SetUp]
        public void BaseSetup() => WriteOutput = "";

        [NotNull] protected Action<string> write  => i => WriteOutput += i;
        protected Act<Option<string>> writeoption => Act.From( (Option<string> i) => WriteOutput += i.ValueOr("NONE"));

        protected static void ThrowAssert<TException>(TestDelegate testDelegate, bool shouldThrow, [CanBeNull] string message = null) where TException : Exception {
            if (shouldThrow) {
                if (!message.IsNullOrEmpty())
                    Assert.That(testDelegate, Throws.TypeOf<TException>().With.Message.EqualTo(message));
                else
                    Assert.That(testDelegate, Throws.TypeOf<TException>());
            } else {
                Assert.That(testDelegate, Throws.Nothing);
            }
        }
    }
}