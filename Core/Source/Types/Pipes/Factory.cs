using System.Collections.Generic;

namespace SharpPipe {
    public static partial class Pipe {
        internal static Pipe<T> From<T>(T obj) => new Pipe<T>(obj);
        internal static Pipe<T> From<T>(Option<T> option) => new Pipe<T>(option);
        internal static Seq<T>  From<T>(IEnumerable<T> obj) => new Seq<T>(obj);
    }
}