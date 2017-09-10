using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using JetBrains.Annotations;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    [TestFixture]
    public abstract class TestBase {
        [NotNull] protected readonly string[] abcarray = {"A", "B", "C"};
        [NotNull] protected List<string>      abclist  => abcarray.ToList();
        
        protected Seq<string>                 abcseq         => start.str.pipe | abcarray;
        protected Pipe<string>                testpipe       => start.str.pipe | "TEST";
        
        
        
        protected string WriteOutput;

        [SetUp]
        public void BaseSetup() => WriteOutput = "";

        [NotNull] protected Action<string> write => i => WriteOutput += i.To<string>("Write");

        protected void ThrowAssert<TException>(TestDelegate testDelegate, bool shouldThrow, string message = null) where TException : Exception {
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