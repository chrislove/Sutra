using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class ThrowTests : TestBase {
        [Test]
        public void Test_Null_Filtered_DoesntThrow() {
            void TestDelegate() {
                var pipe = ABCSeq
                           | add   | (string) null
                           | where | notempty
                           | fail  | when | any(isempty);
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_Throw_With_Message() {
            void TestDelegate() {
                var pipe = ABCSeq
                           | add  | (string) null
                           | failwith("TEST") | when | any(isempty);
                ;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Throw_With_Exception() {
            void TestDelegate() {
                var pipe = ABCSeq
                           | add  | (string) null
                           | fail | new PipeUserException("TEST") | when | any(isempty);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
        }
        
        [Test]
        public void Test_Pipe_Throw_NonConditional() {
            void TestDelegate() {
                var pipe = TestPipe
                           | fail | when | (() => true);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Seq_Throw_NonConditional() {
            void TestDelegate() {
                var pipe = ABCSeq
                           | fail | when | (() => true);
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCSeq
                           | fail | when | any(equals(ifInput));
            }
            
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
    }
}