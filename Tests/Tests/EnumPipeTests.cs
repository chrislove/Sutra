using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using static SharpPipe.Pipe;

// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable PossibleMultipleEnumeration

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumPipeTests : TestBase {
        [NotNull] private static SharpFunc<int, string> ConvertToString => _(( int i ) => i.ToString());

        [Test]
        public void Test_Pipe_Action() {
            var pipe =
                ENUM<int>()
                + Enumerable.Range(0, 3)
                | ConvertToString
                | CONCAT("")
                | ~WriteLine;

            const string expectedOutput = "012";
            Assert.That(WriteLineOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Test_FuncComposition() {
            IEnumerable<string> GetEnumFuncA( string i ) => Enumerable.Repeat(i, 2);
            IEnumerable<string> GetEnumFuncB( string i ) => Enumerable.Repeat(i, 3);

            string enumPipeStr = ENUM<string>()
                                 + GetEnumFuncA("A")
                                 + GetEnumFuncB("B")
                                 | CONCAT("")
                                 | OUT;

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }

        [Test]
        public void Test_IEnumerableComposition() {
            string enumPipeStr = ENUM<string>()
                                 + Enumerable.Repeat("A", 2)
                                 + Enumerable.Repeat("B", 3)
                                 | CONCAT("")
                                 | OUT;

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }

        [Test]
        public void Test_EnumInFunc() {
            EnumInFunc<string, string> CONCAT( string separator )
                => ENUM<string, string>( enumerable => enumerable.Aggregate("", ( a, b ) => a + b + separator) );

            
        }
    }
}