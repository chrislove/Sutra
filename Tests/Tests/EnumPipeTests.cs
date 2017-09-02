using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SharpPipe.Pipe;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable PossibleMultipleEnumeration

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

            string enumPipeStr = ENUM<string>()
                              + getEnumFuncA("A")
                              + getEnumFuncB("B")
                              | ~CONCAT("");

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }
        
        [Test]
        public void Test_EnumerablePipe_IEnumerableComposition() {
            string enumPipeStr = ENUM<string>()
                              + Enumerable.Repeat("A", 2)
                              + Enumerable.Repeat("B", 3)
                              | ~CONCAT("");

            Assert.That(enumPipeStr, Is.EqualTo("AABBB"));
        }

        [Test]
        public void Test_EnumerablePipe_Decomposition() {
            var enumerable = Enumerable.Repeat("A", 3);
            
            var pipe = ENUM<string>()
                       + enumerable;
            
            string str        = pipe | ~CONCAT("");
            List<string> list = pipe | ~TOLIST;
            string[] array    = pipe | ~TOARRAY;
            
            Assert.That(str, Is.EqualTo("AAA"));
            Assert.That(list, Is.EqualTo( enumerable.ToList() ));
            Assert.That(array, Is.EqualTo( enumerable.ToArray() ));
        }
    }
}