using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class ThrowTests : TestBase {
        [Test]
        public void Test_Seq_Null_Throws() {
            void TestDelegate() {
                var pipe = abcseq
                           | add
                           | (string) null
                           | fail | whenany | isnull;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Null_Filtered_DoesntThrow() {
            void TestDelegate() {
                var pipe = abcseq
                           | add   | (string) null
                           | where | notnull
                           | fail | whenany | isnull;
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_Throw_With_Message() {
            void TestDelegate() {
                var pipe = abcseq
                           | add  | (string) null
                           | fail | "TEST" | whenany | isnull;
                ;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Throw_With_Exception() {
            void TestDelegate() {
                var pipe = abcseq
                           | add  | (string) null
                           | fail | new PipeUserException("TEST") | whenany | isnull;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Pipe_Throw_NonConditional() {
            void TestDelegate() {
                var pipe = testpipe
                           | fail | when | (() => true);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Seq_Throw_NonConditional() {
            void TestDelegate() {
                var pipe = abcseq
                           | fail | when | (() => true);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Throw_NextException() {
            void TestDelegate() {
                PIPE.NextException = new PipeUserException("TEST");
                
                var pipe = abcseq
                           | add   | (string) null
                           | fail | whenany | isnull;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = abcseq
                           | fail | whenany | equals(ifInput);
            }
            
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
    }
}