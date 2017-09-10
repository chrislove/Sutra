using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.Curried.str;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class SeqCommandAddTests : TestBase {
        [Test]
        public void Test_ADD_IEnumerable() {
            var defEnumerable = new[] {"D", "E", "F"}.Select(i => i);
            var xyzPipe       = new[] {"X", "Y", "Z"} | to.str.pipe;


            var result = abcseq
                         | add | defEnumerable
                         | add | new[] {"G", "H", "I"}
                         | add | xyzPipe
                         | concat | ret;
            
            Assert.That(result, Is.EqualTo("ABCDEFGHIXYZ"));
        }
    }
}