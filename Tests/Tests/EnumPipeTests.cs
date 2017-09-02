using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumPipeTests {
        [Test]
        public void Test_EnumPipeAction() {
            _(
              ENUM<int>()
              + Enumerable.Range(0, 10)
              + Enumerable.Range(10, 10)
              | WriteLine
             );
        }

        [Test]
        public void Test_EnumerablePipe_FuncComposition() {
            Func<string, IEnumerable<string>> getEnumFuncA = i => Enumerable.Repeat(i, 2);
            Func<string, IEnumerable<string>> getEnumFuncB = i => Enumerable.Repeat(i, 3);

            var enumPipeStr = ENUM<string>()
                              + getEnumFuncA("A")
                              + getEnumFuncB("B")
                              | CONCAT("");

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }
        
        [Test]
        public void Test_EnumerablePipe_IEnumerableComposition() {
            IEnumerable<string> GetEnumA(string i) => Enumerable.Repeat(i, 2);
            IEnumerable<string> GetEnumB(string i) => Enumerable.Repeat(i, 3);

            var enumPipeStr = ENUM<string>()
                              + GetEnumA("A")
                              + GetEnumB("B")
                              | CONCAT("");

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }
    }
}