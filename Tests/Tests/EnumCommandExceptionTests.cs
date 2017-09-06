using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandExceptionTests : TestBase {
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCPipe
                           | THROW | IF | (i => i == ifInput);
            }
            
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
        
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowExceptionIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCPipe
                           //| THROW * EXC("throws") * IF(ifInput);
                           | THROW | IF | (i => i == ifInput);
            }
            
            //ThrowAssert<PipeUserException>(TestDelegate, shouldThrow, "throws");
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIfException(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCPipe
                           //| THROW | IF(i => i == ifInput) * EXC("throws");
                           | THROW | IF | (i => i == ifInput);
            }
            
            //ThrowAssert<PipeUserException>(TestDelegate, shouldThrow, "throws");
            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
    }
}