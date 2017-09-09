using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.Curry.STRING;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class ConversionTests : TestBase {
        [Test]
        public void Test_Value_ToPipe() {
            string result = START.STRING.PIPE | "IN" | OUT;
            
            Assert.That(result, Is.EqualTo("IN"));
        }
        
        [Test]
        public void Test_List_ToPipe() {
            string result = ABCList | TO.STRING.PIPE | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }

        [Test]
        public void Test_Pipe_ToEnumerable() {
            IEnumerable<string> Converter( string str ) => Enumerable.Range(0, 3).Select(i => str + i);

            string result = START.STRING.PIPE | "IN"
                            | TO.ENUMERABLE | Converter
                            | Join(";") | OUT;
            
            Assert.That(result, Is.EqualTo("IN0;IN1;IN2"));
        }
        
        [Test]
        public void Test_Array_ToEnumerable() {
            string result = ABCArray | TO.STRING.PIPE
                            | Concat | OUT;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }
    }
}