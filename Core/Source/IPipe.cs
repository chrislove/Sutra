namespace SharpPipe {
    internal interface IPipe<T> {
        bool AllowNullOutput { get; }
    }
}