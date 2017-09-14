using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpPipe.CurryLib;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class ConversionTests : TestBase {
        [Test]
        public void Test_List_ToPipe() {
            string result = start.str.seq | ABCList | str.concat | !get;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }

        [Test]
        public void Test_Pipe_ToEnumerable() {
            Func<int, IEnumerable<string>> converter = inval => Enumerable.Range(0, 3)
                                                                        .Select(i => inval + i)
                                                                        .Select(i => i.ToString());

            string result = start.integer.pipe | 100
                            | to.seq(converter)
                            | str.join(";") | !get;
            
            Assert.That(result, Is.EqualTo("100;101;102"));
        }
        
        [Test]
        public void Test_Array_ToEnumerable() {
            string result = start.str.seq | ABCArray
                            | str.concat  | !get;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }
    }
}