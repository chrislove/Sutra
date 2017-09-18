using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests
{
    public sealed class ThrowTests : TestBase
    {
        [Test]
        public void Test_Null_Filtered_DoesntThrow()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | add   | (string) null
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
                                           | add | (string) null
                                           | failwith("fail $seq") | when | any(isempty);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("fail Seq<String>"));
            }

        [Test]
        public void Test_Throw_With_Exception()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | add | (string) null
                                           | fail | new PipeUserException("TEST") | when | any(isempty);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("TEST"));
            }

        [Test]
        public void Test_Seq_Throw_NonConditional()
            {
                void TestDelegate()
                    {
                        Seq<string> seq = ABCSeq
                                           | fail | when | (() => true);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
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

                ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
            }
    }
}