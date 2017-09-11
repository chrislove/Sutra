using System.IO;
using static SharpPipe.Commands;

namespace SharpPipe {
    namespace CurryLib {
        public static class path {
            /// <summary>
            /// Uses Path.Combine to append path.
            /// </summary>
            public static PipeFunc<string, string> append( string append )  => func.str.from( instr => Path.Combine(instr, append) );
            
            /// <summary>
            /// Uses Path.Combine to prepend path.
            /// </summary>
            public static PipeFunc<string, string> prepend( string prepend ) => func.str.from( instr => Path.Combine(prepend, instr) );


            public static PipeFunc<string, string> changeextension(string extension) => func.str.from( path => Path.ChangeExtension(path, extension) );

            public static PipeFunc<string, string> getdirectoryname => func.str.from(Path.GetDirectoryName);
            public static PipeFunc<string, string> getextension => func.str.from(Path.GetExtension);
            public static PipeFunc<string, string> getfullpath => func.str.from(Path.GetFullPath);
            public static PipeFunc<string, string> getfilename => func.str.from(Path.GetFileName);
            public static PipeFunc<string, string> getfilenamewithoutextension => func.str.from(Path.GetFileNameWithoutExtension);
            public static PipeFunc<string, string> getpathroot => func.str.from(Path.GetPathRoot);
            
            public static PipeFunc<string, bool> hasextension => func.str.from(Path.HasExtension);
            public static PipeFunc<string, bool> ispathrooted => func.str.from(Path.IsPathRooted);

            public static char[] getinvalidpathchars => Path.GetInvalidPathChars();
            public static char[] getinvalidfilenamechars => Path.GetInvalidFileNameChars();
            public static string gettemppath => Path.GetTempPath();
            public static string getrandomfilename => Path.GetRandomFileName();
            public static string gettempfilename => Path.GetTempFileName();
        }
    }
}