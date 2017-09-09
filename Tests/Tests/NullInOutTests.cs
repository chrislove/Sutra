using System.Collections.Generic;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class NullInOutTests : TestBase {
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void Test_Pipe_NullIn_Throws(bool nullInAllowed, bool shouldThrow) {
            void TestDelegate() {
                PIPE.AllowNullInput = nullInAllowed;
                
                var pipe = NEW.STRING.PIPE
                           | (string) null;
            }

            ThrowAssert<NullPipeException>(TestDelegate, shouldThrow);
        }
        
        [Test]
        public void Test_Enum_NullIn_Throws() {
            void TestDelegate() {
                var pipe = NEW.STRING.PIPE
                           | (IEnumerable<string>) null;
            }

            Assert.That(TestDelegate, Throws.TypeOf<NullPipeException>());
        }
        
        [Test]
        public void Test_Enum_NullTransform_Throws() {
            void TestDelegate() {
                PIPE.AllowNullInput = true;

                var pipe = ABCEnumPipe
                           | TRANSFORM | (i => (IEnumerable<string>) null);
            }

            Assert.That(TestDelegate, Throws.TypeOf<NullPipeException>());
        }
        
        [Test]
        public void Test_Pipe_NullOut_Throws() {
            void TestDelegate() {
                PIPE.AllowNullInput = true;
                
                var pipe = TestPipe
                           | (i => null)
                           | OUT;
            }

            Assert.That(TestDelegate, Throws.TypeOf<NullPipeException>());
        }
        
        [Test]
        public void Test_Pipe_AllowNullOut_DoesntThrow() {
            void TestDelegate() {
                PIPE.AllowNullInput = true;
                
                var pipe = TestPipe
                           | (i => null)
                           | ALLOWNULL
                           | OUT;
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
    }
}