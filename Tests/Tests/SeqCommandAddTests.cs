using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.CurryLib.str;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class SeqCommandAddTests : TestBase {
        [Test]
        public void Test_ADD_IEnumerable() {
            var defEnumerable = new[] {"D", "E", "F"}.Select(i => i);
            var xyzPipe       = start.str.seq | new[] {"X", "Y", "Z"};


            var result = ABCSeq
                         | add | defEnumerable
                         | add | new[] {"G", "H", "I"}
                         | add | xyzPipe
                         | concat | !get;
            
            Assert.That(result, Is.EqualTo("ABCDEFGHIXYZ"));
        }
    }
}