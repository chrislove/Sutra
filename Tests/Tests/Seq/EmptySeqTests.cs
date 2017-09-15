using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe.Tests
{
    public sealed class EmptySeqTests : TestBase
    {
        [Test]
        public void Test_NoneValue_ReturnsEmptyOption()
            {
                Option<IEnumerable<string>> option = EmptyTestSeq
                                                     | !get;

                Assert.That(!option.HasValue);
            }
        
        [Test]
        public void Test_NoneValue_ThrowsNothingOnGet2()
            {
                void TestDelegate()
                    {
                        IEnumerable<Option<string>> enm = EmptyTestSeq
                                                          | !!get;
                    }

                Assert.That(TestDelegate, Throws.Nothing);
            }
        
        [Test]
        public void Test_NoneValue_ThrowsOnGet3()
            {
                void TestDelegate()
                    {
                        IEnumerable<string> enm = EmptyTestSeq
                                                          | !!!get;

	                    var list = enm.ToList();	// Needed to make sure that the iterator is called.
                    }

                Assert.That(TestDelegate, Throws.TypeOf<EmptySequenceException>());
            }

        [Test]
        public void Test_NoneValue_SkipsEmptyIter()
            {
                Unit pipe = EmptyTestSeq
                           | iter | write;

                Assert.That(WriteOutput, Is.EqualTo("TEST"));
            }

        [Test]
        public void Test_NoneValue_DoesntSkipOptionIterAction()
            {
                Unit pipe = EmptyTestSeq
                           | iter | writeoption;

                Assert.That(WriteOutput, Is.EqualTo("NONETEST"));
            }

        [Test]
        public void Test_NoneValue_DoesntSkipOptionIterFunc()
            {
                Option<string> Func( Option<string> str )
                    => str.Match(i => i, "NONE").ToOption();

                Unit pipe = EmptyTestSeq
                           | map | Func
                           | iter | writeoption;

                Assert.That(WriteOutput, Is.EqualTo("NONETEST"));
            }
    }
}