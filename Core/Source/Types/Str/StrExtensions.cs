using System.Collections.Generic;
using System.Linq;

namespace Sutra {
    public static class StrExtensions
    {
        public static str Join( this IEnumerable<str> enm, somestr separator ) => string.Join(separator, enm.ToStringEnumerable());
        public static str Concat(this IEnumerable<str> enm) => string.Concat(enm.ToStringEnumerable());

        public static IEnumerable<string> ToStringEnumerable( this IEnumerable<str> enm ) => enm.Select(v => v.ValueOr(""));
    }
}