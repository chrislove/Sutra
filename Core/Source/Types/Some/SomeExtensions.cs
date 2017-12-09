using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Sutra
{
    public static class SomeExtensions
    {
        public static Some<T> ToSome<T>( [NotNull] this T obj,
                                       [CanBeNull] [CallerMemberName] string memberName = null,
                                       [CanBeNull] [CallerFilePath] string filePath = null,
                                       [CallerLineNumber] int lineNumber = 0  ) => new Some<T>(obj, memberName, filePath, lineNumber);
        
        public static Some<T> ToSome<T>( this Option<T> obj,
                                       [CanBeNull] [CallerMemberName] string memberName = null,
                                       [CanBeNull] [CallerFilePath] string filePath = null,
                                       [CallerLineNumber] int lineNumber = 0  ) => new Some<T>(obj, memberName, filePath, lineNumber);
        
        //public static Some<T> Some<T>( [NotNull] this IOption<T> obj ) => new Some<T>(obj);

        public static somestr ToSome( [NotNull] this string str,
                                    [CanBeNull] [CallerMemberName] string memberName = null,
                                    [CanBeNull] [CallerFilePath] string filePath = null,
                                    [CallerLineNumber] int lineNumber = 0 ) => new somestr(str, memberName, filePath, lineNumber);

        public static somestr ToSome( this str str,
                                    [CanBeNull] [CallerMemberName] string memberName = null,
                                    [CanBeNull] [CallerFilePath] string filePath = null,
                                    [CallerLineNumber] int lineNumber = 0 ) => new somestr(str, memberName, filePath, lineNumber);
    }
}