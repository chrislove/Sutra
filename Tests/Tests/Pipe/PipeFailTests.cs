using NUnit.Framework;
using Sutra.CurryLib;
using static Sutra.Conditions;
using static Sutra.Commands;

namespace Sutra.Tests
{
    public sealed class PipeFailTests : TestBase
    {
        [Test]
        public void Test_Pipe_Throw_NonConditional()
            {
                void TestDelegate()
                    {
                        Pipe<string> pipe = TestPipe
                                            | fail | when | (() => true);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<SutraCommandException>());
            }


        [TestCase("TE", false)]
        [TestCase("AB", true)]
        public void Test_Pipe_When_Throws( string instr, bool shouldThrow )
            {
                void TestDelegate()
                    {
                        Pipe<string> pipe = TestPipe
                                            | fail | when | not(strf.contains(instr));
                    }

                ThrowAssert<SutraCommandException>(TestDelegate, shouldThrow);
            }

        [Test]
        public void Test_Pipe_Throw_WithMessage()
            {
                void TestDelegate()
                    {
                        Pipe<string> pipe = TestPipe
                                            | failwith("Fail: $pipe") | when | (() => true);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<SutraUserException>().With.Message.EqualTo("Fail: TEST"));
            }

        /*
        [Test]
        public void Test_Or_WithTwoPipesFail()
        {
            var pipeA = start.str.pipe
                        | 
            
            string str = start.str.pipe
                         | (string) null
                         | or | "ALT"
                         | !get;
    
            Assert.That(str, Is.EqualTo("ALT"));
        }*/
    }
}