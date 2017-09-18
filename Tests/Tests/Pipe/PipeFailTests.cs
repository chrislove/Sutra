using NUnit.Framework;
using SharpPipe.CurryLib;
using static SharpPipe.Commands;

namespace SharpPipe.Tests
{
    public class PipeFailTests : TestBase
    {
        [Test]
        public void Test_Pipe_Throw_NonConditional()
            {
                void TestDelegate()
                    {
                        Pipe<string> pipe = TestPipe
                                            | fail | when | (() => true);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<PipeCommandException>());
            }
        
        

        [TestCase("TE", false)]
        [TestCase("AB", true)]
        public void Test_Pipe_When_Throws(string instr, bool shouldThrow)
            {
                void TestDelegate()
                    {
                        Pipe<string> pipe = TestPipe
                                            | fail | when | not( str.Contains(instr) );
                    }
                
                ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
            }
        
        [Test]
        public void Test_Pipe_Throw_WithMessage()
            {
                void TestDelegate()
                    {
                        Pipe<string> pipe = TestPipe
                                            | failwith("Fail: $pipe") | when | (() => true);
                    }

                Assert.That(TestDelegate, Throws.TypeOf<PipeUserException>().With.Message.EqualTo("Fail: TEST"));
            }
    }
}