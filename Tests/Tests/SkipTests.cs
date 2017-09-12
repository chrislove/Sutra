using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class SkipTests : TestBase {
        [Test]
        public void Test_NullInput_ReturnsNull() {
            var pipe = start.str.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | !get;

            Assert.That(pipe, Is.EqualTo(null));
        }
        
        [Test]
        public void Test_NullInput_SkipsAction() {
            var pipe = start.str.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | act | write;

            Assert.That(WriteOutput, Is.EqualTo(""));
        }
        
        [Test]
        public void Test_NullInput_Fail_Throws() {
            void TestDelegate() {
                var pipe = start.str.pipe
                           | (string) null
                           | fail | when | isnull;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_OptionFunc_DoesNotSkip() {}
    }
}