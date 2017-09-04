using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandTests : TestBase {
        [Test]
        public void Test_Distinct() {
            string pipeStr = ENUM.STR
                             + Enumerable.Repeat("A", 5)
                             + Enumerable.Repeat("B", 10)
                             + Enumerable.Repeat("C", 12)
                             | DISTINCT
                             | CONCAT("")
                             | OUT;

            Assert.That(pipeStr, Is.EqualTo("ABC"));
        }
        
        [Test]
        public void Test_Conversion() {
            var enumerable = Enumerable.Repeat("A", 3);

            var pipe = ENUM.STR
                       + enumerable;

            string str        = pipe | CONCAT("") | OUT;
            List<string> list = pipe | TOLIST     | OUT;
            string[] array    = pipe | TOARRAY    | OUT;

            Assert.That(str,   Is.EqualTo("AAA"));
            Assert.That(list,  Is.EqualTo(enumerable.ToList()));
            Assert.That(array, Is.EqualTo(enumerable.ToArray()));
        }
        
        [Test]
        public void Test_Null_Throws() {
            void TestDelegate() {
                var pipe = ENUM.STR
                           + "A" + "B" + "C" + (string) null
                           | THROW & IFNULL;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [Test]
        public void Test_Null_Filtered_DoesntThrow() {
            void TestDelegate() {
                var pipe = ENUM.STR
                           + "A" + "B" + "C" + (string) null
                           | NOTNULL
                           | THROW & IFNULL;
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
        
        [Test]
        public void Test_Filter() {
            string result = ENUM.STR
                         + "A" + "B" + "C"
                         | FILTER(i => i != "B")
                         | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("AC"));
        }
    }
}