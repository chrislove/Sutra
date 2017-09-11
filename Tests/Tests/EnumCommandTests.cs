using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.CurryLib.str;

namespace SharpPipe.Tests {
    public sealed class SeqCommandTests : TestBase {
        [Test]
        public void Test_Distinct() {
            string pipeStr = start.str.pipe
                             | add | Enumerable.Repeat("A", 5)
                             | add | Enumerable.Repeat("B", 10)
                             | add | Enumerable.Repeat("C", 12)
                             | distinct
                             | concat
                             | ret;

            Assert.That(pipeStr, Is.EqualTo("ABC"));
        }
        
        [Test]
        public void Test_Conversion() {
            var enumerable = Enumerable.Repeat("A", 3);

            var pipe = start.str.pipe
                       | add | enumerable;

            string str        = pipe | concat     | ret;
            List<string> list = pipe | retlist;
            string[] array    = pipe | retarray;

            Assert.That(str,   Is.EqualTo("AAA"));
            Assert.That(list,  Is.EqualTo(enumerable.ToList()));
            Assert.That(array, Is.EqualTo(enumerable.ToArray()));
        }
        
        [Test]
        public void Test_Where() {
            string result = ABCSeq
                         | where | notequals("B")
                         | concat | ret;
            
            Assert.That(result, Is.EqualTo("AC"));
        }
        
        [Test]
        public void Test_Select() {
            string result = ABCSeq
                         | select | (i => $"[{i}]")
                         | concat | ret;
            
            Assert.That(result, Is.EqualTo("[A][B][C]"));
        }
        
        [Test]
        public void Test_Single() {
            string result = ABCSeq
                            | where | equals("B")
                            | single | ret;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_First() {
            string result = ABCSeq | add | ABCSeq
                            | where | equals("B")
                            | first | ret;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_SelectMany() {
            IEnumerable<string> SelectManyFunc( string str ) => Enumerable.Repeat(str, 3);
            
            string result = ABCSeq
                         | selectmany | SelectManyFunc
                         | concat | ret;
            
            Assert.That(result, Is.EqualTo("AAABBBCCC"));
        }
        
        [Test]
        public void Test_Append_NewPipe() {
            string result = start.str.pipe | ABCArray
                         | append | "D" | "E" | "F" | end
                         | concat | ret;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
        
        [Test]
        public void Test_Transform() {
            IEnumerable<string> TransformFunc( IEnumerable<string> enumerable )
                => enumerable.Select(i => i + ";");
            
            string result = ABCSeq
                            | transform | TransformFunc
                            | concat | ret;
            
            Assert.That(result, Is.EqualTo("A;B;C;"));
        }
        
        [Test]
        public void Test_IfEmpty_Throws() {
            void TestDelegate() {
                var pipe = start.str.pipe
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
                var emptyPipe = start.str.pipe
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