using NUnit.Framework;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class PipeSkipTests : TestBase {
        [Test]
        public void Test_NullInput_ReturnsEmptyOption() {
            var pipe = start.str.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | get;

            Assert.That(!pipe.HasValue);
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
        public void Test_NullInput_DoesntSkipOptionAction() {
            var pipe = start.str.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | act | writeoption;

            Assert.That(WriteOutput, Is.EqualTo("NONE"));
        }
        
        [Test]
        public void Test_NullInput_DoesntSkipOptionFunc() {
            Option<string> Func( Option<string> str )
                => str.Match(i => i, "NONE").ToOption();
            
            var pipe = start.str.pipe
                       | (string) null
                       | (i => i + "TEST1")
                       | (i => i + "TEST2")
                       | Func
                       | act | writeoption;

            Assert.That(WriteOutput, Is.EqualTo("NONE"));
        }
        
        [Test]
        public void Test_NullInput_Fail_Throws() {
            void TestDelegate() {
                var pipe = start.str.pipe
                           | null
                           | fail | when | isempty;
            }

            Assert.That(TestDelegate, Throws.TypeOf<EmptyPipeException>());
        }
    }
}