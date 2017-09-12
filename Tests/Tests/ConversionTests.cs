using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Commands;
using static SharpPipe.CurryLib.str;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class ConversionTests : TestBase {
        [Test]
        public void Test_Value_ToPipe() {
            string result = start.str.pipe | "IN" | !get;
            
            Assert.That(result, Is.EqualTo("IN"));
        }
        
        [Test]
        public void Test_List_ToPipe() {
            string result = start.str.seq | ABCList | concat | !get;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }

        [Test]
        public void Test_Pipe_ToEnumerable() {
            IEnumerable<string> converter( string str ) => Enumerable.Range(0, 3).Select(i => str + i);

            string result = start.str.pipe | "IN"
                            | to.seq | converter
                            | join(";") | !get;
            
            Assert.That(result, Is.EqualTo("IN0;IN1;IN2"));
        }
        
        [Test]
        public void Test_Array_ToEnumerable() {
            string result = start.str.seq | ABCArray
                            | concat | !get;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }
    }
}