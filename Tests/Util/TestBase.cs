using NUnit.Framework;

namespace SharpPipe.Tests {
    [TestFixture]
    public abstract class TestBase {
        protected string WriteLineOutput;

        protected SharpAct<object> WriteLine => SharpAct.FromAction<object>( i => WriteLineOutput = i.To<string>() );
    }
}