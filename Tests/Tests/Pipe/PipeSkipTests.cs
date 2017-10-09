using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class PipeSkipTests : TestBase {
        [Test]
        public void Test_NullInput_ReturnsEmptyOption() {
            var pipe = start.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | get;

            Assert.That(!pipe.HasValue);
        }
        
        [Test]
        public void Test_NullInput_SkipsAction() {
            var pipe = start.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | tee | write;

            Assert.That(WriteOutput, Is.EqualTo(""));
        }
        
        [Test]
        public void Test_NullInput_DoesntSkipOptionAction() {
            var pipe = start.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | tee | writeoption;

            Assert.That(WriteOutput, Is.EqualTo("NONE"));
        }
        
        [Test]
        public void Test_NullInput_DoesntSkipOptionFunc() {
                Option<string> Func( Option<string> option ) => option.Map(i => i, "NONE");

                var pipe = start.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | Func
                       | tee | writeoption;

            Assert.That(WriteOutput, Is.EqualTo("NONE"));
        }
        
        [Test]
        public void Test_NullInput_Fail_Throws() {
            void TestDelegate() {
                var pipe = start.pipe
                           | null
                           | fail | when | isempty;
            }

            Assert.That(TestDelegate, Throws.TypeOf<EmptyPipeException>());
        }
    }
}