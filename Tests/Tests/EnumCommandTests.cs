using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.CurryLib.str;

namespace SharpPipe.Tests {
    public sealed class SeqCommandTests : TestBase {
        [Test]
        public void Test_Distinct() {
            string pipeStr = start.str.seq
                             | add | Enumerable.Repeat("A", 5)
                             | add | Enumerable.Repeat("B", 10)
                             | add | Enumerable.Repeat("C", 12)
                             | distinct
                             | concat
                             | !get;

            Assert.That(pipeStr, Is.EqualTo("ABC"));
        }
        
        [Test]
        public void Test_Conversion() {
            var enumerable = Enumerable.Repeat("A", 3);

            var pipe = start.str.seq
                       | add | enumerable;

            string str        = pipe | concat     | !get;
            List<string> list = pipe | getlist;
            string[] array    = pipe | getarray;

            Assert.That(str,   Is.EqualTo("AAA"));
            Assert.That(list,  Is.EqualTo(enumerable.ToList()));
            Assert.That(array, Is.EqualTo(enumerable.ToArray()));
        }
        
        [Test]
        public void Test_Where() {
            string result = ABCSeq
                         | where | notequals("B")
                         | concat | !get;
            
            Assert.That(result, Is.EqualTo("AC"));
        }
        
        [Test]
        public void Test_Select() {
            string result = ABCSeq
                         | map | (i => $"[{i}]")
                         | concat | !get;
            
            Assert.That(result, Is.EqualTo("[A][B][C]"));
        }
        
        [Test]
        public void Test_Single() {
            string result = ABCSeq
                            | where  | equals("B")
                            | single | !get;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_First() {
            string result = ABCSeq | add | ABCSeq
                            | where | equals("B")
                            | first | !get;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_SelectMany() {
            IEnumerable<string> SelectManyFunc( string str ) => Enumerable.Repeat(str, 3);
            
            string result = ABCSeq
                         | collect | SelectManyFunc
                         | concat  | !get;
            
            Assert.That(result, Is.EqualTo("AAABBBCCC"));
        }
        
        [Test]
        public void Test_Append_NewPipe() {
            string result = start.str.seq | ABCArray
                         | add | "D" | "E" | "F"
                         | concat | !get;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
        
        [Test]
        public void Test_Transform() {
            IEnumerable<string> TransformFunc( IEnumerable<string> enumerable )
                => enumerable.Select(i => i + ";");
            
            string result = ABCSeq
                            | TransformFunc
                            | concat | !get;
            
            Assert.That(result, Is.EqualTo("A;B;C;"));
        }
        
        [Test]
        public void Test_IfEmpty_Throws() {
            void TestDelegate() {
                var pipe = start.str.seq
                           | add   | ""
                           | where | notequals("")
                           | fail  | when | isempty;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [TestCase(true, new string[0])]
        [TestCase(true, new []{"A", "B", "C"})]
        [TestCase(false, new []{"A"})]
        public void Test_IsNotSingle(bool shouldThrow, string[] testStrings) {
            void TestDelegate() {
                var emptyPipe = start.str.seq
                                | add  | testStrings
                                | fail | when | isnotsingle;
            }

            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
        
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void Test_NotEmpty(bool isEmpty, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCSeq
                           | where | (i => !isEmpty)
                           | notempty;
            }

            ThrowAssert<EmptyPipeException>(TestDelegate, shouldThrow);
        }
    }
}