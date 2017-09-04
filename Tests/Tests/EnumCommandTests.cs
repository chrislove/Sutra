using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandTests : TestBase {
        [Test]
        public void Test_Distinct() {
            string pipeStr = ENUM<string>()
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

            var pipe = ENUM<string>()
                       + enumerable;

            string str        = pipe | CONCAT("") | OUT;
            List<string> list = pipe | TOLIST     | OUT;
            string[] array    = pipe | TOARRAY    | OUT;

            Assert.That(str,   Is.EqualTo("AAA"));
            Assert.That(list,  Is.EqualTo(enumerable.ToList()));
            Assert.That(array, Is.EqualTo(enumerable.ToArray()));
        }
    }
}