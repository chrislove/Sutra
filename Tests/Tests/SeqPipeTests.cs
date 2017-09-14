using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.CurryLib.str;

// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable PossibleMultipleSeqeration

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class SequenceTests : TestBase {
        private static PipeFunc<int, string> ConvertToString => func.integer.from(i => i.ToString());

        [Test]
        public void Test_Pipe_Action() {
            var pipe = start.integer.seq
                | add | Enumerable.Range(0, 3)
                | ConvertToString
                | concat
                | act | write;

            const string expectedOutput = "012";
            Assert.That(WriteOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Test_FuncComposition() {
            IEnumerable<string> GetSeqA( string i ) => Enumerable.Repeat(i, 2);
            IEnumerable<string> GetSeqB( string i ) => Enumerable.Repeat(i, 3);

            string enumPipeStr = start.str.seq
                                 | add | GetSeqA("A")
                                 | add | GetSeqB("B")
                                 | concat
                                 | !get;

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }

        [Test]
        public void Test_IEnumerableComposition() {
            string enumPipeStr = start.str.seq
                                 | add | Enumerable.Repeat("A", 2)
                                 | add | Enumerable.Repeat("B", 3)
                                 | concat
                                 | !get;

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }
        
        [Test]
        public void Test_Foreach() {
            var pipe = ABCSeq
                       | iter | write;

            Assert.That(WriteOutput, Is.EqualTo("ABC"));
        }
    }
}