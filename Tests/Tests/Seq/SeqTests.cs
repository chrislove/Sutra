﻿using System.Linq;
using NUnit.Framework;
using static Sutra.Commands;
using static Sutra.CurryLib.strf;

// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable PossibleMultipleSeqeration

namespace Sutra.Tests {
    [TestFixture]
    public sealed class SeqTests : TestBase {
        private static PipeFunc<int, string> ConvertToString => fun((int i) => i.ToString());

        [Test]
        public void Test_Seq_Action() {
                var pipe = start.seq
                            | Enumerable.Range(0, 3)
                            | map | ConvertToString
                            | concat
                            | tee | write;

            const string expectedOutput = "012";
            Assert.That(WriteOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Test_IEnumerableComposition() {
            string result = start.seq | Enumerable.Repeat("A", 2)
                                 | append | Enumerable.Repeat("B", 3)
                                 | concat
                                 | !get;

            Assert.That(result, Is.EqualTo("AABBB"));
        }
        
        [Test]
        public void Test_Iter() {
            var pipe = ABCSeq
                        | iter | write;

            Assert.That(WriteOutput, Is.EqualTo("ABC"));
        }
    }
}