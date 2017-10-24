using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests
{
    public sealed class OptionTests : TestBase
    {
        [Test]
        public void Test_StrOrEmpty()
            {
                string str = "" | opt | "" | get;

                Assert.That(str == "");
            }
    }
}