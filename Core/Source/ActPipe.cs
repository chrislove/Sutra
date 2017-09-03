namespace SharpPipe {
    public static class ActPipe {
        public static ActPipe<T> FromPipe<T>(Pipe<T> pipe) => new ActPipe<T>( pipe.Func );
    }
}