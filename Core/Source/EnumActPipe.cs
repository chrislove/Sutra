namespace SharpPipe {
    public static class EnumActPipe {
        public static EnumActPipe<T> FromPipe<T>(EnumPipe<T> pipe) => new EnumActPipe<T>( pipe.Func );
    }
}