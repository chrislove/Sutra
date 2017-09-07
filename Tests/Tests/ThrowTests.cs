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
                           | THROW | IFANY | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Null_Filtered_DoesntThrow() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | WHERE | NOTNULL
                           | THROW | IFANY | ISNULL;
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_Throw_With_Message() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | THROW | "TEST" | IFANY | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Throw_With_Exception() {
            void TestDelegate() {
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | THROW | new PipeUserException("TEST") | IFANY | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Throw_NextException() {
            void TestDelegate() {
                PIPE.NextException = new PipeUserException("TEST");
                
                var pipe = ABCPipe
                           | ADD   | (string) null
                           | THROW | IFANY | ISNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCPipe
                           | THROW | IFANY | IS(ifInput);
            }
            
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
        
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowExceptionIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCPipe
                           | THROW | IFANY | IS(ifInput);
            }
            
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
    }
}