using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.Pipe;

// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable PossibleMultipleEnumeration

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumPipeTests : TestBase {
        private static SharpFunc<int, string> ConvertToString => _(( int i ) => i.ToString());

        [Test]
        public void Test_Pipe_Action() {
            var pipe =
                ENUM.INT
                | ADD | Enumerable.Range(0, 3)
                | ConvertToString
                | CONCAT("")
                | ACT | Write;

            const string expectedOutput = "012";
            Assert.That(WriteOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Test_FuncComposition() {
            IEnumerable<string> GetEnumA( string i ) => Enumerable.Repeat(i, 2);
            IEnumerable<string> GetEnumB( string i ) => Enumerable.Repeat(i, 3);

            string enumPipeStr = ENUM.STR
                                 | ADD | GetEnumA("A")
                                 | ADD | GetEnumB("B")
                                 | CONCAT("")
                                 | OUT;

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }

        [Test]
        public void Test_IEnumerableComposition() {
            string enumPipeStr = ENUM.STR
                                 | ADD | Enumerable.Repeat("A", 2)
                                 | ADD | Enumerable.Repeat("B", 3)
                                 | CONCAT("")
                                 | OUT;

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }
        
        [Test]
        public void Test_Foreach() {
            var pipe = ABCPipe
                       | FOREACH | Write;

            Assert.That(WriteOutput, Is.EqualTo("ABC"));
        }

        [Test]
        public void Test_Transform_With_IEnumerableFunction() {
        //public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, Func<IEnumerable<TOut>, IEnumerable<TOut>> func ) {

        }
        
        [Test]
        public void Test_Transform_With_NormalFunction() {
        //public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, Func<IEnumerable<TOut>, IEnumerable<TOut>> func ) {

        }
    }
}