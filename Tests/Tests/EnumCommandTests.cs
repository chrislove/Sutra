using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class EnumCommandTests : TestBase {
        [Test]
        public void Test_Distinct() {
            string pipeStr = ENUM.STR
                             | ADD | Enumerable.Repeat("A", 5)
                             | ADD | Enumerable.Repeat("B", 10)
                             | ADD | Enumerable.Repeat("C", 12)
                             | DISTINCT
                             | CONCAT("")
                             | OUT;

            Assert.That(pipeStr, Is.EqualTo("ABC"));
        }
        
        [Test]
        public void Test_Conversion() {
            var enumerable = Enumerable.Repeat("A", 3);

            var pipe = ENUM.STR
                       | ADD | enumerable;

            string str        = pipe | CONCAT("") | OUT;
            List<string> list = pipe | TOLIST     | OUT;
            string[] array    = pipe | TOARRAY    | OUT;

            Assert.That(str,   Is.EqualTo("AAA"));
            Assert.That(list,  Is.EqualTo(enumerable.ToList()));
            Assert.That(array, Is.EqualTo(enumerable.ToArray()));
        }
        
        [Test]
        public void Test_Where() {
            string result = ABCPipe
                         | WHERE | ISNOT("B")
                         | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("AC"));
        }
        
        [Test]
        public void Test_Select() {
            string result = ABCPipe
                         | SELECT | (i => $"[{i}]")
                         | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("[A][B][C]"));
        }
        
        [Test]
        public void Test_Single() {
            string result = ABCPipe
                            | WHERE | IS("B")
                            | SINGLE | OUT;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_First() {
            string result = ABCPipe | ADD | ABCPipe
                            | WHERE | IS("B")
                            | FIRST | OUT;
            
            Assert.That(result, Is.EqualTo("B"));
        }
        
        [Test]
        public void Test_SelectMany() {
            IEnumerable<string> SelectManyFunc( string str ) => Enumerable.Repeat(str, 3);
            
            string result = ABCPipe
                         | SELECTMANY | SelectManyFunc
                         | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("AAABBBCCC"));
        }
        
        [Test]
        public void Test_Append() {
            string result = ABCPipe
                         | APPEND | "D" | "E" | "F" | I
                         | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("ABCDEF"));
        }
        
        [Test]
        public void Test_Transform() {
            IEnumerable<string> TransformFunc( IEnumerable<string> enumerable )
                => enumerable.Select(i => i + ";");
            
            string result = ABCPipe
                            | TRANSFORM | TransformFunc
                            | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("A;B;C;"));
        }
        
        [Test]
        public void Test_IfEmpty_Throws() {
            void TestDelegate() {
                var pipe = ENUM<string>.NEW
                           | THROW | IF | ISEMPTY;
            }

            Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
        }
        
        [TestCase(true, new string[0])]
        [TestCase(true, new []{"A", "B", "C"})]
        [TestCase(false, new []{"A"})]
        public void Test_IsNotSingle(bool shouldThrow, string[] testStrings) {
            void TestDelegate() {
                var emptyPipe = ENUM<string>.NEW | ADD | testStrings
                                | THROW | IF | ISNOTSINGLE;
            }

            ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
        }
    }
}