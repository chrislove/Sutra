using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandExceptionTests : TestBase {
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ENUM.STR
                           + "A" + "B" + "C"
                           | THROW & IF(ifInput);
            }
            
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowExceptionIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ENUM.STR
                           + "A" + "B" + "C"
                           | THROW & EXC("throws") & IF(ifInput);
            }
            
            ThrowAssert<PipeUserException>(TestDelegate, shouldThrow, "throws");
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIfException(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ENUM.STR
                           + "A" + "B" + "C"
                           | THROWIF(i => i == ifInput) & EXC("throws");
            }
            
            ThrowAssert<PipeUserException>(TestDelegate, shouldThrow, "throws");
        }
    }
}