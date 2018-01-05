using System.Collections.Generic;
using NUnit.Framework;
using static Sutra.Commands;
using static Sutra.Conditions;

namespace Sutra.Tests
{
    public sealed class PqipeFuncInteroperabilityTests : TestBase
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Test_Seq_AnyNot( bool shouldThrow )
        {
            var paths = new List<string> {@"C:\TestDir\TestFile.cs"};

            if (shouldThrow)
                paths.Add(@"SomeRandomDir\File.cs");

            void TestDelegate()
            {
                Seq<string> seq = start.seq
                                  | paths
                                  | fail | when | any(fun(s => s.Contains(@"SomeRandomDir\File.cs")));
            }

            ThrowAssert<SutraCommandException>(TestDelegate, shouldThrow);
        }
    }
}