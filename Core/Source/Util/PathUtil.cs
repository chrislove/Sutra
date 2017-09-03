using System.IO;
using static SharpPipe.Pipe;

namespace SharpPipe {
    /// <summary>
    /// A wrapper around System.IO.Path.
    /// </summary>
    public static class PathUtil {
        public static SharpFunc<string, string> ChangeExtension  ( string extension )  => _<string>(path    => Path.ChangeExtension(path, extension) );
        
        public static SharpFunc<string, string> CombineAppend    ( string b )  => _<string>(a => Path.Combine(a, b) );
        public static SharpFunc<string, string> CombinePrepend   ( string a )  => _<string>(b => Path.Combine(a, b) );
        
        public static SharpFunc<string, string> GetDirectoryName	           => _<string>(Path.GetDirectoryName);
        public static SharpFunc<string, string> GetExtension		           => _<string>(Path.GetExtension);
        public static SharpFunc<string, string> GetFileName		               => _<string>(Path.GetFileName);
        public static SharpFunc<string, string> GetFileNameWithoutExtension    => _<string>(Path.GetFileNameWithoutExtension);
        public static SharpFunc<string, string> GetFullPath		               => _<string>(Path.GetFullPath);
        public static SharpFunc<string, string> GetPathRoot		               => _<string>(Path.GetPathRoot);
        
        public static SharpFunc<string, bool>   HasExtension		           => _<string, bool>(Path.HasExtension);
        public static SharpFunc<string, bool>   IsPathRooted	               => _<string, bool>(Path.IsPathRooted);
        
        
        
        public static Pipe<string>              GetRandomFileName		       => IN(Path.GetRandomFileName());
        public static Pipe<string>              GetTempFileName		           => IN(Path.GetTempFileName());
        public static Pipe<string>              GetTempPath		               => IN(Path.GetTempPath());
        public static Pipe<char[]>              GetInvalidPathChars		       => IN(Path.GetInvalidPathChars());
        public static Pipe<char[]>              GetInvalidFileNameChars	       => IN(Path.GetInvalidFileNameChars());
    }
}