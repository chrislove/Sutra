using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
    public static partial class Pipe {
        public static DoConcat CONCAT(string separator) => new DoConcat(separator);
    }
    
    public struct DoConcat {
        [CanBeNull] internal readonly string Separator;
		
        internal DoConcat( [CanBeNull] string separator ) {
            Separator = separator;
        }

        /// <summary>
        /// Pipe decomposition operator.
        /// Returns the concatenated value.
        /// </summary>
        public static ToValue operator ~( DoConcat x ) {
            return new ToValue(x.Separator);
        }

        public struct ToValue {
            public static partial class Pipe {
                public static DoToList TOLIST => new DoToList();
            }            [CanBeNull] internal readonly string Separator;
		
            internal ToValue( [CanBeNull] string separator ) {
                Separator = separator;
            }
        }
    }

    public partial class EnumPipe<TOut> {
        /// <summary>
        /// Concatenates pipe contents into a string
        /// </summary>
        [NotNull]
        public static Pipe<string> operator |( EnumPipe<TOut> lhs, DoConcat act ) {
            if (typeof(TOut) != typeof(string))
                throw new TypeMismatchException("CONCAT can only be done on EnumPipe<string>");

            string str = lhs.Get.Aggregate("", ( a, b ) => a + b + act.Separator);

            return IN(str);
        }
        
        /// <summary>
        /// Concatenates pipe contents into a string
        /// </summary>
        [NotNull]
        public static string operator |( EnumPipe<TOut> lhs, DoConcat.ToValue act ) => lhs | CONCAT(act.Separator) | OUT;
    }
}