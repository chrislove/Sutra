using System.Collections.Generic;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Appends a value to EnumPipe{T}.
        /// </summary>
        public static DoStartAdd ADD => new DoStartAdd();
        public static DoAdd IN([CanBeNull] object obj) => new DoAdd(obj);
    }

    public struct DoStartAdd {
        public static DoAdd operator *( DoStartAdd lhs, [CanBeNull] object obj ) => new DoAdd(obj);
    }

    public struct DoAdd {
        internal readonly object Obj;

        internal DoAdd( [CanBeNull] object obj ) => Obj = obj;
    }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator -( EnumPipe<TOut> lhs, DoAdd act ) {           
            if (act.Obj is TOut[])
                return lhs + act.Obj.To<TOut[]>();
            
            if (act.Obj is IEnumerable<TOut>)
                return lhs + act.Obj.To<IEnumerable<TOut>>();
            
            if (act.Obj is EnumPipe<TOut>)
                return lhs + act.Obj.To<EnumPipe<TOut>>();
            
            return lhs + act.Obj.To<TOut>();
        }
    }
}