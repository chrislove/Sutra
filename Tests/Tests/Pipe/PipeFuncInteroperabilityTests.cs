using System.Collections.Generic;
using NUnit.Framework;
using Sutra.CurryLib;
using static Sutra.Commands;
using static Sutra.Conditions;

namespace Sutra.Tests
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
                                          | fail | when | any( not(pathf.ispathrooted) );
                    }
                
                ThrowAssert<SutraCommandException>(TestDelegate, shouldThrow);
            }
    }
}