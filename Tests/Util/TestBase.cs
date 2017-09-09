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
        [NotNull] protected readonly string[] ABCArray = {"A", "B", "C"};
        [NotNull] protected List<string>      ABCList  => ABCArray.ToList();
        
        protected EnumPipe<string>  ABCEnumPipe  => NEW.STRING.PIPE | ABCArray;
        protected Pipe<string>      TestPipe     => NEW.STRING.PIPE | "TEST";
        
        
        
        protected string WriteOutput;

        [SetUp]
        public void BaseSetup() => WriteOutput = "";

        [NotNull] protected Action<string> Write => i => WriteOutput += i.To<string>("Write");

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