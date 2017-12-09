using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static Sutra.Conditions;
using static Sutra.Commands;
using static Sutra.CurryLib.strf;

namespace Sutra.Tests
{
    public sealed class SeqCommandTests : TestBase
    {
        [Test]
        public void Test_Distinct()
            {
                string pipeStr = start.seq.of<string>()
                                 | append | Enumerable.Repeat("A", 5)
                                 | append | Enumerable.Repeat("B", 10)
                                 | append | Enumerable.Repeat("C", 12)
                                 | distinct
                                 | concat
                                 | !get;

                Assert.That(pipeStr, Is.EqualTo("ABC"));
            }

        [Test]
        public void Test_Conversion()
            {
                IEnumerable<string> enumerable = Enumerable.Repeat("A", 3);

                Seq<string> pipe = start.seq | enumerable;

                string str = pipe | concat | !get;
                List<string> list = pipe | getlist;
                string[] array = pipe | getarray;

                Assert.That(str, Is.EqualTo("AAA"));
                Assert.That(list, Is.EqualTo(enumerable.ToList()));
                Assert.That(array, Is.EqualTo(enumerable.ToArray()));
            }

        [Test]
        public void Test_Where()
            {
                string result = ABCSeq
                                | where | notequals("B")
                                | where | (i => !i.Contains("B"))
                                | concat | !get;

                Assert.That(result, Is.EqualTo("AC"));
            }

        [Test]
        public void Test_Select()
            {
                string result = ABCSeq
                                | map | (i => $"[{i}]")
                                | concat | !get;

                Assert.That(result, Is.EqualTo("[A][B][C]"));
            }

        [Test]
        public void Test_Single()
            {
                string result = ABCSeq
                                | where | equals("B")
                                | single | !get;

                Assert.That(result, Is.EqualTo("B"));
            }

        [Test]
        public void Test_First()
            {
                string result = ABCSeq | append | ABCSeq
                                | where | equals("B")
                                | first | !get;

                Assert.That(result, Is.EqualTo("B"));
            }

        [Test]
        public void Test_Collect()
            {
                IEnumerable<string> func( string str ) => Enumerable.Repeat(str, 3);

                string result = ABCSeq
                                | collect | func
                                | concat | !get;

                Assert.That(result, Is.EqualTo("AAABBBCCC"));
            }

        [Test]
        public void Test_Collectf_NoEmpty()
            {
                Func<int, IEnumerable<string>> func = i => Enumerable.Repeat(i.ToString(), 3);

                string result = start.seq | Enumerable.Range(0, 3)
                                | collectf(func)
                                | concat | !get;

                Assert.That(result, Is.EqualTo("000111222"));
            }

        [Test]
        public void Test_Collectf_HasEmpty()
            {
                Func<int, IEnumerable<string>> func = i => Enumerable.Repeat(i.ToString(), 3);

                str str = start.seq | Enumerable.Range(0, 3)
                          | append | default(Option<int>)
                          | collectf(func)
                          | concat | get;

                Assert.That(!str.HasValue);
            }

        [Test]
        public void Test_Add_Objects()
            {
                string result = start.seq | ABCArray
                                | "D" | "E" | "F"
                                | concat | !get;

                Assert.That(result, Is.EqualTo("ABCDEF"));
            }

        [Test]
        public void Test_Transform()
            {
                IEnumerable<string> TransformFunc( IEnumerable<string> enumerable )
                    => enumerable.Select(i => i + ";");

                string result = ABCSeq
                                | TransformFunc
                                | concat | !get;

                Assert.That(result, Is.EqualTo("A;B;C;"));
            }

        [Test]
        public void Test_IfEmpty_Throws()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = start.seq.of<string>()
                                          | append | "A"
                                          | where | notequals("A")
                                          | fail | when | isempty;
                    }

                Assert.That(TestDelegate, Throws.TypeOf<EmptySequenceException>());
            }

        [TestCase(true, new string[0])]
        [TestCase(true, new[] {"A", "B", "C"})]
        [TestCase(false, new[] {"A"})]
        public void Test_IsNotSingle( bool shouldThrow, string[] testStrings )
            {
                void TestDelegate()
                    {
                        Seq<string> emptyPipe = start.seq
                                                | testStrings
                                                | fail | when | notsingle;
                    }

                ThrowAssert<SutraCommandException>(TestDelegate, shouldThrow);
            }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void Test_NotEmpty( bool isEmpty, bool shouldThrow )
            {
                void TestDelegate()
                    {
                        Seq<string> pipe = ABCSeq
                                           | where | (( string i ) => !isEmpty)
                                           | fail | when | isempty;
                    }

                ThrowAssert<EmptySequenceException>(TestDelegate, shouldThrow);
            }

        [Test]
        public void Test_Append_IEnumerable()
            {
                IEnumerable<string> defEnumerable = new[] {"D", "E", "F"}.Select(i => i);
                Seq<string> xyzSeq = start.seq | new[] {"X", "Y", "Z"};


                string result = ABCSeq
                                | append | defEnumerable
                                | append | new[] {"G", "H", "I"}
                                | append | xyzSeq
                                | append | "@"
                                | concat | !get;

                Assert.That(result, Is.EqualTo("ABCDEFGHIXYZ@"));
            }
    }
}