using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;

namespace SharpPipe.Tests {
    [TestFixture]
    public sealed class EnumPipeTests {
        [Test]
        public void TestEnumPipeAction() {
            _(
              ENUM<int>()
              + Enumerable.Range(0, 10)
              + Enumerable.Range(10, 10)
              | WriteLine
             );
        }

        [Test]
        public void TestEnumerablePipeComposition() {
            Func<string, IEnumerable<string>> getEnumFuncA = i => Enumerable.Repeat(i, 2);
            Func<string, IEnumerable<string>> getEnumFuncB = i => Enumerable.Repeat(i, 3);

            var enumPipeStr = ENUM<string>()
                              + getEnumFuncA("A")
                              + getEnumFuncB("B")
                              | CONCAT("");

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }
    }
}