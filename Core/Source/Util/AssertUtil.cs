using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace SharpPipe {
    [PublicAPI]
    internal static class AssertUtil
    {
        public sealed class AssertionException : Exception
        {
            public AssertionException( [CanBeNull] string msg ) : base(msg) { }
        
            public static AssertionException Create<T>(T value, string name, string message, string memberName, string filePath, int lineNumber) where T : class
                {
                    string nameStr    = name    != null ? $"Value '{name}'" : "Value";
                    string messageStr = message != null ? $". {message}" : "";
                
                    return new AssertionException($"{nameStr} of type '{typeof(T).FullName}' is null at {memberName}, {filePath}:{lineNumber} {messageStr}");
                }
        
            public static AssertionException CreateForStringOrEnumerable<T>(T value, string name, string message, string memberName, string filePath, int lineNumber)
                {
                    string nameStr    = name    != null ? $"{typeof(T)} '{name}'" : typeof(T).ToString();
                    string messageStr = message != null ? $". {message}" : "";
                
                    return new AssertionException($"{nameStr} is null or empty at {memberName}, {filePath}:{lineNumber} {messageStr}");
                }
        }

        [NotNull]
        [AssertionMethod]
        [PublicAPI]
        public static T ThrowIfNull<T>( [CanBeNull] T value,
                                        [CanBeNull] string name = null,
                                        [CanBeNull] string message = null,
                                        [CanBeNull] [CallerMemberName] string memberName = null,
                                        [CanBeNull] [CallerFilePath] string filePath = null,
                                        [CallerLineNumber] int lineNumber = 0 ) where T : class
            {
                if (value == null)
                    throw AssertionException.Create(value, name, message, memberName, filePath, lineNumber);

                return value;
            }

        [NotNull]
        [AssertionMethod]
        [PublicAPI]
        public static string ThrowIfNullOrEmpty( [CanBeNull] string value,
                                                 [CanBeNull] string name = null,
                                                 [CanBeNull] string message = null,
                                                 [CanBeNull] [CallerMemberName] string memberName = null,
                                                 [CanBeNull] [CallerFilePath] string filePath = null,
                                                 [CallerLineNumber] int lineNumber = 0 )
            {
                if (string.IsNullOrEmpty(value))
                    throw AssertionException.CreateForStringOrEnumerable(value, name, message, memberName, filePath, lineNumber);

                return value; 
            }
    
        [NotNull]
        [AssertionMethod]
        [PublicAPI]
        public static string ThrowIfNullOrWhiteSpace( [CanBeNull] string value,
                                                      [CanBeNull] string name = null,
                                                      [CanBeNull] string message = null,
                                                      [CanBeNull] [CallerMemberName] string memberName = null,
                                                      [CanBeNull] [CallerFilePath] string filePath = null,
                                                      [CallerLineNumber] int lineNumber = 0 )
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw AssertionException.CreateForStringOrEnumerable(value, name, message, memberName, filePath, lineNumber);

                return value; 
            }
    
        [NotNull]
        [AssertionMethod]
        [PublicAPI]
        public static IEnumerable<T> ThrowIfNullOrEmpty<T>( [CanBeNull] IEnumerable<T> enm,
                                                            [CanBeNull] string name = null,
                                                            [CanBeNull] string message = null,
                                                            [CanBeNull] [CallerMemberName] string memberName = null,
                                                            [CanBeNull] [CallerFilePath] string filePath = null,
                                                            [CallerLineNumber] int lineNumber = 0 )
            {
                if (enm == null || !enm.Any())
                    throw AssertionException.CreateForStringOrEnumerable(enm, name, message, memberName, filePath, lineNumber);
            
                return enm; 
            }
    }
}