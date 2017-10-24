namespace SharpPipe {
    public static class SomeExtensions
    {
        public static Some<T> Some<T>(this T obj) => new Some<T>(obj);
        public static Some<T> Some<T>(this Option<T> obj) => new Some<T>(obj);
        
        public static somestr Some(this string str) => new somestr(str);
    }
}