using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpPipe {
    /// <summary>
    /// A wrapper around System.IO.Path.
    /// </summary>
    public static class PathUtil {
        public static Func<string, string> PathAppend    ( string b )  => a => Path.Combine(a, b);
        public static Func<string, string> PathPrepend   ( string a )  => b => Path.Combine(a, b);
        
        /// <summary>
        /// EnumPipe commands
        /// </summary>
        public static class E {
            public static Func<IEnumerable<string>, IEnumerable<string>> CombineAppend    ( string b )
                                                                            => inenum => inenum.Select(PathUtil.PathAppend(b));
            
            public static Func<IEnumerable<string>, IEnumerable<string>> CombinePrepend( string a )
                                                                            => inenum => inenum.Select(PathUtil.PathPrepend(a));
        }
    }
}