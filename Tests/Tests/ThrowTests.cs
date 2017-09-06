using System;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class ThrowTests : TestBase {
        [Test]
        public void Test_Null_Throws() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD
                           | (string) null
                           | THROW | IF | (i => i == null);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Null_Filtered_DoesntThrow() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD | (string) null
                           | WHERE | (i => i != null)
                           | THROW | IF | (i => i == null);
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_Throw_With_Message() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD | (string) null
                           | THROW | "TEST" | IF | (i => i == null);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Throw_With_Exception() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD | (string) null
                           | THROW | new PipeUserException("TEST") | IF | (i => i == null);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
    }
}