using System;
using Castle.Core.Internal;
using NUnit.Framework;

namespace SharpPipe.Tests {
    [TestFixture]
    public abstract class TestBase {
        protected string WriteLineOutput;

        protected SharpAct<object> WriteLine => SharpAct.FromAction<object>( i => WriteLineOutput = i.To<string>() );

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