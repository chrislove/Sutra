using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct DoConcat{
        public struct ToValue {
            [CanBeNull] private readonly string Separator;
		
            internal ToValue( [CanBeNull] string separator ) => Separator = separator;
                    
            /// <summary>
            /// Concatenates pipe contents into a string
            /// </summary>
            [NotNull]
            public static string operator |( EnumPipe<string> lhs, ToValue act ) => lhs | Pipe.CONCAT(act.Separator) | Pipe.OUT;
        }
    }
}