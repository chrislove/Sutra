namespace SharpPipe {
    public interface IPipe {}
    public interface IPipe<T> : IPipe {}

    internal static class PipeExtensions
    {
        public static Pipe<T> ToPipe<T>( this IPipe<T> pipe ) => (Pipe<T>) pipe;
        public static Seq<T> ToSeq<T>( this IPipe<T> pipe )   => (Seq<T>) pipe;
    }
}