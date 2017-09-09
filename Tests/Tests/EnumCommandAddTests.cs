using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.Curry.STRING;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumCommandAddTests : TestBase {
        [Test]
        public void Test_ADD_IEnumerable() {
            var defEnumerable = new[] {"D", "E", "F"}.Select(i => i);
            var xyzPipe       = new[] {"X", "Y", "Z"} | TO.STRING.PIPE;


            var result = ABCEnumerablePipe
                         | ADD | defEnumerable
                         | ADD | new[] {"G", "H", "I"}
                         | ADD | xyzPipe
                         | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEFGHIXYZ"));
        }
    }
}