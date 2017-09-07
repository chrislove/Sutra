using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class ConversionTests : TestBase {
        [Test]
        public void Test_Value_ToPipe() {
            string result = "IN" | TO<string>() | OUT;
            
            Assert.That(result, Is.EqualTo("IN"));
        }
        
        [Test]
        public void Test_List_ToPipe() {
            string result = ABCArray.ToList() | TO<string>() | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }

        [Test]
        public void Test_Pipe_ToEnum() {
            IEnumerable<string> Converter( string str ) => Enumerable.Range(0, 3).Select(i => str + i);

            string result = "IN" | TO<string>()
                            | ENUM.TO | Converter
                            | CONCAT(";") | OUT;
            
            Assert.That(result, Is.EqualTo("IN0;IN1;IN2;"));
        }
        
        [Test]
        public void Test_Array_ToEnum() {
            string result = ABCArray | TO<string>()
                            | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }
    }
}