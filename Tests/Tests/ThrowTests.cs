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
                           | THROW | IF | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Null_Filtered_DoesntThrow() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | WHERE | NOTNULL
                           | THROW | IF | ISNULL;
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_Throw_With_Message() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | THROW | "TEST" | IF | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Throw_With_Exception() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | THROW | new PipeUserException("TEST") | IF | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
    }
}