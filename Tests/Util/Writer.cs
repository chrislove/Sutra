using JetBrains.Annotations;

namespace SharpPipe.Tests {
    [UsedImplicitly]
    public class Writer {
        public virtual SharpAct<object> WriteLine => Pipe.WriteLine;
    }
}