using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandAddTests : TestBase {
        [Test]
        public void Test_ADD_WithIncompatibleType_Throws() {
            void TestDelegate() {
                var pipe = ENUM<string>()
                           | ADD & 0;
            }
            
            Assert.That(TestDelegate, Throws.TypeOf<TypeMismatchException>());
        }
        
        [Test]
        public void Test_ADD_WithCompatibleType_DoesntThrow() {
            void TestDelegate() {
                var pipe = ENUM<string>()
                           | ADD & "test";
            }
            
            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_ADD_IEnumerable() {
            var enumerable = new[] {"D", "E", "F"}.Select(i => i);

            string result = ENUM<string>()
                            + "A" + "B" + "C"
                            | ADD & enumerable
                            | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
        
        [Test]
        public void Test_ADD_Array() {
            string result = ENUM<string>()
                            + "A" + "B" + "C"
                            | ADD & new[] {"D", "E", "F"}
                            | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
        
        [Test]
        public void Test_ADD_EnumPipe() {
            var testPipe = ENUM<string>()
                           + new[] {"D", "E", "F"};
            
            string result = ENUM<string>()
                            + "A" + "B" + "C"
                            | ADD & testPipe
                            | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
    }
}