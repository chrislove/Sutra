using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.Curry.STRING;

namespace SharpPipe.Tests {
    public sealed class EnumCommandTests : TestBase {
        [Test]
        public void Test_Distinct() {
            string pipeStr = START.STRING.PIPE
                             | ADD | Enumerable.Repeat("A", 5)
                             | ADD | Enumerable.Repeat("B", 10)
                             | ADD | Enumerable.Repeat("C", 12)
                             | DISTINCT
                             | Concat
                             | OUT;

            Assert.That(pipeStr, Is.EqualTo("ABC"));
        }
        
        [Test]
        public void Test_Conversion() {
            var enumerable = Enumerable.Repeat("A", 3);

            var pipe = START.STRING.PIPE
                       | ADD | enumerable;

            string str        = pipe | Concat     | OUT;
            List<string> list = pipe | TOLIST     | OUT;
            string[] array    = pipe | TOARRAY    | OUT;

            Assert.That(str,   Is.EqualTo("AAA"));
            Assert.That(list,  Is.EqualTo(enumerable.ToList()));
            Assert.That(array, Is.EqualTo(enumerable.ToArray()));
        }
        
        [Test]
        public void Test_Where() {
            string result = ABCEnumerablePipe
                         | WHERE | ISNOT("B")
                         | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("AC"));
        }
        
        [Test]
        public void Test_Select() {
            string result = ABCEnumerablePipe
                         | SELECT | (i => $"[{i}]")
                         | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("[A][B][C]"));
        }
        
        [Test]
        public void Test_Single() {
            string result = ABCEnumerablePipe
                            | WHERE | IS("B")
                            | SINGLE | OUT;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_First() {
            string result = ABCEnumerablePipe | ADD | ABCEnumerablePipe
                            | WHERE | IS("B")
                            | FIRST | OUT;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_SelectMany() {
            IEnumerable<string> SelectManyFunc( string str ) => Enumerable.Repeat(str, 3);
            
            string result = ABCEnumerablePipe
                         | SELECTMANY | SelectManyFunc
                         | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("AAABBBCCC"));
        }
        
        [Test]
        public void Test_Append_NewPipe() {
            string result = START.STRING.PIPE | ABCArray
                         | APPEND | "D" | "E" | "F" | END
                         | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
        
        [Test]
        public void Test_Transform() {
            IEnumerable<string> TransformFunc( IEnumerable<string> enumerable )
                => enumerable.Select(i => i + ";");
            
            string result = ABCEnumerablePipe
                            | TRANSFORM | TransformFunc
                            | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("A;B;C;"));
        }
        
        [Test]
        public void Test_IfEmpty_Throws() {
            void TestDelegate() {
                var pipe = START.STRING.PIPE
                           | ADD   | ""
                           | WHERE | ISNOT("")
                           | THROW | IF | ISEMPTY;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [TestCase(true, new string[0])]
        [TestCase(true, new []{"A", "B", "C"})]
        [TestCase(false, new []{"A"})]
        public void Test_IsNotSingle(bool shouldThrow, string[] testStrings) {
            void TestDelegate() {
                var emptyPipe = START.STRING.PIPE
                                | ADD | testStrings
                                | THROW | IF | ISNOTSINGLE;
            }

            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
        
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void Test_NotEmpty(bool isEmpty, bool shouldThrow) {
            void TestDelegate() {
                var pipe = ABCEnumerablePipe
                           | WHERE | (i => !isEmpty)
                           | NOTEMPTY;
            }

            ThrowAssert<EmptyPipeException>(TestDelegate, shouldThrow);
        }
    }
}