using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandExceptionTests : TestBase {
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ENUM<string>()
                           + "A" + "B" + "C"
                           | THROW & IF(ifInput);
            }
            
            if (shouldThrow)
                Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
            else
                Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowExceptionIf(string ifInput, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ENUM<string>()
                           + "A" + "B" + "C"
                           | THROW & EXC("throws") & IF(ifInput);
            }
            
            if (shouldThrow)
                Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("throws"));
            else
                Assert.That(TestDelegate, Throws.Nothing);
        }
    }
}