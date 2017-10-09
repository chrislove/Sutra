using System.Collections.Generic;
using NUnit.Framework;
using SharpPipe.CurryLib;
using static SharpPipe.Commands;

namespace SharpPipe.Tests
{
    public sealed class PqipeFuncInteroperabilityTests : TestBase
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Test_Seq_AnyNot(bool shouldThrow)
            {
                var paths = new List<string> {@"C:\TestDir\TestFile.cs"};
                
                if (shouldThrow)
                    paths.Add(@"SomeRandomDir\File.cs");

                void TestDelegate()
                    {
                        Seq<string> seq = start.seq
                                          | paths
                                          | fail | when | any( not(path.ispathrooted) );
                    }
                
                ThrowAssert<PipeCommandException>(TestDelegate, shouldThrow);
            }
    }
}