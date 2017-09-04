using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters null objects out from the EnumPipe{T}
        /// </summary>
        public static DoNotNull NOTNULL => new DoNotNull();
    }
    
    public struct DoNotNull {}
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator -( EnumPipe<TOut> lhs, DoNotNull act ) => lhs - WHERE(i => i != null);
    }
}