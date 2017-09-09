using System;
using System.IO;

namespace SharpPipe {
    public static partial class Curry {
        public static class PATH {
            /// <summary>
            /// Uses Path.Combine to append path.
            /// </summary>
            public static Func<string, string> PathAppend( string append )  => instr => Path.Combine(instr, append);
            
            /// <summary>
            /// Uses Path.Combine to prepend path.
            /// </summary>
            public static Func<string, string> PathPrepend( string prepend ) => instr => Path.Combine(prepend, instr);


            public static Func<string, string> ChangeExtension(string extension) => path => Path.ChangeExtension(path, extension);

            public static Func<string, string> GetDirectoryName => Path.GetDirectoryName;
            public static Func<string, string> GetExtension => Path.GetExtension;
            public static Func<string, string> GetFullPath => Path.GetFullPath;
            public static Func<string, string> GetFileName => Path.GetFileName;
            public static Func<string, string> GetFileNameWithoutExtension => Path.GetFileNameWithoutExtension;
            public static Func<string, string> GetPathRoot => Path.GetPathRoot;

            public static char[] GetInvalidPathChars => Path.GetInvalidPathChars();
            public static char[] GetInvalidFileNameChars => Path.GetInvalidFileNameChars();
            public static string GetTempPath => Path.GetTempPath();
            public static string GetRandomFileName => Path.GetRandomFileName();
            public static string GetTempFileName => Path.GetTempFileName();
            
            public static Func<string, bool> HasExtension => Path.HasExtension;
            public static Func<string, bool> IsPathRooted => Path.IsPathRooted;
        }
    }
}