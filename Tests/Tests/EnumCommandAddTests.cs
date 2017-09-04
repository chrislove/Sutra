using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandAddTests : TestBase {
        [Test]
        public void Test_ADD_WithIncompatibleType_Throws() {
            void TestDelegate() {
                var pipe = ENUM.STR
                           - IN(0);
            }
            
            Assert.That(TestDelegate, Throws.TypeOf<TypeMismatchException>());
        }
        
        [Test]
        public void Test_ADD_WithCompatibleType_DoesntThrow() {
            void TestDelegate() {
                var pipe = ENUM.STR
                           - ADD * "test"
                           - IN("test");
            }
            
            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_ADD_IEnumerable() {
            var enumerable = new[] {"D", "E", "F"}.Select(i => i);

            var result = ENUM.STR
                         + "A" + "B" + "C"
                         - IN(enumerable)
                         - ADD * enumerable
                         - CONCAT("") - OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEFDEF"));
        }
        
        [Test]
        public void Test_ADD_Array() {
            string result = ENUM.STR
                            + "A" + "B" + "C"
                            - IN( new[] {"D", "E", "F"} )
                            - ADD * new[] {"D", "E", "F"}
                            - CONCAT("") - OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEFDEF"));
        }
        
        [Test]
        public void Test_ADD_EnumPipe() {
            var testPipe = ENUM.STR
                           + new[] {"D", "E", "F"};
            
            string result = ENUM.STR
                            + "A" + "B" + "C"
                            - IN(testPipe)
                            - ADD * testPipe
                            - CONCAT("") - OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEFDEF"));
        }
    }
}