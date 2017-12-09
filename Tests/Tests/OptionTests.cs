using NUnit.Framework;
using static Sutra.Commands;

namespace Sutra.Tests
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