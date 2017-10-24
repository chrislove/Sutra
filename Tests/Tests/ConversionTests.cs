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
            string result = start.seq | ABCList | strf.concat | !get;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }

        [Test]
        public void Test_Pipe_ToEnumerable() {
            Func<int, IEnumerable<string>> converter = inval => Enumerable.Range(0, 3)
                                                                        .Select(i => inval + i)
                                                                        .Select(i => i.ToString());

            string result = start.pipe | 100
                            | to.seq(converter)
                            | strf.join(";") | !get;
            
            Assert.That(result, Is.EqualTo("100;101;102"));
        }
        
        [Test]
        public void Test_Array_ToEnumerable() {
            string result = start.seq | ABCArray
                            | strf.concat  | !get;
            
            Assert.That(result, Is.EqualTo("ABC"));
        }
    }
}