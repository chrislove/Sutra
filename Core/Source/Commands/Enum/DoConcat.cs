using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Pipe;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Pipe {
        public static DoConcat CONCAT(string separator) => new DoConcat(separator);
    }

    public partial struct DoConcat {
        [CanBeNull] private readonly string Separator;

        internal DoConcat( [CanBeNull] string separator ) => Separator = separator;

        /// <summary>
        /// Concatenates pipe contents into a string
        /// </summary>
        public static Pipe<string> operator |( EnumPipe<string> lhs, DoConcat act ) {
            string str = lhs.Get.Aggregate("", ( a, b ) => a + b + act.Separator);

            return IN(str);
        }
    }
}