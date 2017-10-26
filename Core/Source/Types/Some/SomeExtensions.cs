using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace SharpPipe {
    public static class SomeExtensions
    {
        public static Some<T> Some<T>([NotNull] this T obj) => new Some<T>(obj);
        public static Some<T> Some<T>([NotNull] this IOption<T> obj) => new Some<T>(obj);
        
        public static somestr Some([NotNull] this string str,
                                   [CanBeNull] [CallerMemberName] string memberName = null,
                                   [CanBeNull] [CallerFilePath] string filePath = null,
                                   [CallerLineNumber] int lineNumber = 0 ) => new somestr(str, memberName, filePath, lineNumber);
    }
}