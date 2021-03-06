using NUnit.Framework;
using static Sutra.Conditions;
using static Sutra.Commands;

namespace Sutra.Tests
{
    public sealed class ThrowTests : TestBase
    {
        [Test]
        public void Test_Null_Filtered_DoesntThrow()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | append   | (string) null
                                           | where | notempty
                                           | fail  | when | any(isempty);
                    }

                Assert.That(TestDelegate, Throws.Nothing);
            }

        [Test]
        public void Test_Throw_With_Message()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | append | (string) null
                                           | failwith("fail $seq") | when | any(isempty);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<SutraUserException>().With.Message.EqualTo("fail Seq<String>"));
            }

        [Test]
        public void Test_Throw_With_Exception()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | append  | (string) null
                                           | fail | new SutraUserException("TEST") | when | any(isempty);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<SutraUserException>().With.Message.EqualTo("TEST"));
            }

        [Test]
        public void Test_Seq_Throw_NonConditional()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | fail | when | (() => true);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<SutraCommandException>());
            }

        [TestCase("B", true)]
        [TestCase("DONT", false)]
        public void Test_ThrowIf( string ifInput, bool shouldThrow )
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | fail | when | any(equals(ifInput));
                    }

                ThrowAssert<SutraCommandException>(TestDelegate, shouldThrow);
            }
    }
}