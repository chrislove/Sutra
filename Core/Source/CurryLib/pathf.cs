using System.IO;
using JetBrains.Annotations;
using static Sutra.Commands;

namespace Sutra {
    namespace CurryLib {
        [PublicAPI]
        public static class pathf {
            /// <summary>
            /// Uses Path.Combine to append path.
            /// </summary>
            public static PipeFunc<string, string> append( string append )  => fun(instr => Path.Combine(instr, append) );
            
            /// <summary>
            /// Uses Path.Combine to prepend path.
            /// </summary>
            public static PipeFunc<string, string> prepend( string prepend ) => fun(instr => Path.Combine(prepend, instr) );


            public static PipeFunc<string, string> changeextension(string extension) => fun(path => Path.ChangeExtension(path, extension) );

            public static PipeFunc<string, string> getdirectoryname => fun(Path.GetDirectoryName);
            public static PipeFunc<string, string> getextension => fun(Path.GetExtension);
            public static PipeFunc<string, string> getfullpath => fun(Path.GetFullPath);
            public static PipeFunc<string, string> getfilename => fun(Path.GetFileName);
            public static PipeFunc<string, string> getfilenamewithoutextension => fun(Path.GetFileNameWithoutExtension);
            public static PipeFunc<string, string> getpathroot => fun(Path.GetPathRoot);
            
            public static PipeFunc<string, bool> hasextension => fun(Path.HasExtension);
            public static PipeFunc<string, bool> ispathrooted => fun(Path.IsPathRooted);

            public static char[] getinvalidpathchars => Path.GetInvalidPathChars();
            public static char[] getinvalidfilenamechars => Path.GetInvalidFileNameChars();
            public static string gettemppath => Path.GetTempPath();
            public static string getrandomfilename => Path.GetRandomFileName();
            public static string gettempfilename => Path.GetTempFileName();
        }
    }
}