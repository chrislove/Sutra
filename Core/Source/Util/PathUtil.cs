using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpPipe {
    /// <summary>
    /// A wrapper around System.IO.Path.
    /// </summary>
    public static class PathUtil {
        ///public static Func<string, string> ChangeExtension  ( string extension )  => path    => Path.ChangeExtension(path, extension);
        
        public static Func<string, string> CombineAppend    ( string b )  => a => Path.Combine(a, b);
        public static Func<string, string> CombinePrepend   ( string a )  => b => Path.Combine(a, b);
        
        /*
        public static Func<string, string> GetDirectoryName	              => Path.GetDirectoryName;
        public static Func<string, string> GetExtension		              => Path.GetExtension;
        public static Func<string, string> GetFileName		              => Path.GetFileName;
        public static Func<string, string> GetFileNameWithoutExtension    => Path.GetFileNameWithoutExtension;
        public static Func<string, string> GetFullPath		              => Path.GetFullPath;
        public static Func<string, string> GetPathRoot		              => Path.GetPathRoot;
        
        public static Func<string, bool>   HasExtension		              => Path.HasExtension;
        public static Func<string, bool>   IsPathRooted	                  => Path.IsPathRooted;
        
        public static Pipe<string>              GetRandomFileName		       => PIPE.STR | Path.GetRandomFileName();
        public static Pipe<string>              GetTempFileName		           => PIPE.STR | Path.GetTempFileName();
        public static Pipe<string>              GetTempPath		               => PIPE.STR | Path.GetTempPath();
        
        public static Pipe<char[]>              GetInvalidPathChars		       => PIPE.IN( Path.GetInvalidPathChars() );
        public static Pipe<char[]>              GetInvalidFileNameChars	       => PIPE.IN( Path.GetInvalidFileNameChars() );*/
        
        /// <summary>
        /// EnumPipe commands
        /// </summary>
        public static class E {
            public static Func<IEnumerable<string>, IEnumerable<string>> CombineAppend    ( string b )
                                                                            => inenum => inenum.Select(PathUtil.CombineAppend(b));
            
            public static Func<IEnumerable<string>, IEnumerable<string>> CombinePrepend( string a )
                                                                            => inenum => inenum.Select(PathUtil.CombinePrepend(a));
        }
    }
}