using System;
using System.IO;

namespace SharpPipe {
    public static partial class Curried {
        public static class path {
            /// <summary>
            /// Uses Path.Combine to append path.
            /// </summary>
            public static Func<string, string> append( string append )  => instr => Path.Combine(instr, append);
            
            /// <summary>
            /// Uses Path.Combine to prepend path.
            /// </summary>
            public static Func<string, string> prepend( string prepend ) => instr => Path.Combine(prepend, instr);


            public static Func<string, string> changeextension(string extension) => path => Path.ChangeExtension(path, extension);

            public static Func<string, string> getdirectoryname => Path.GetDirectoryName;
            public static Func<string, string> getextension => Path.GetExtension;
            public static Func<string, string> getfullpath => Path.GetFullPath;
            public static Func<string, string> getfilename => Path.GetFileName;
            public static Func<string, string> getfilenamewithoutextension => Path.GetFileNameWithoutExtension;
            public static Func<string, string> getpathroot => Path.GetPathRoot;

            public static char[] getinvalidpathchars => Path.GetInvalidPathChars();
            public static char[] getinvalidfilenamechars => Path.GetInvalidFileNameChars();
            public static string gettemppath => Path.GetTempPath();
            public static string getrandomfilename => Path.GetRandomFileName();
            public static string gettempfilename => Path.GetTempFileName();
            
            public static Func<string, bool> hasextension => Path.HasExtension;
            public static Func<string, bool> ispathrooted => Path.IsPathRooted;
        }
    }
}