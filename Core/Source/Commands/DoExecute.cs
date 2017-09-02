// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
    public static partial class Pipe {
        // ReSharper disable once MemberCanBePrivate.Global
        public static DoExecute DO => new DoExecute();

        public struct VOID {
            internal static VOID New => new VOID();
        }
    }
    
    public partial struct Pipe<TOut> {
        public static VOID operator |( Pipe<TOut> lhs, DoExecute act ) {
            act.Action(lhs.Get);
            
            return VOID.New;
        }
    }

    public partial struct EnumPipe<TOut> {       
        /*
        /// <summary>
        /// Performs an action on every item of the EnumPipe
        /// </summary>
        [UsedImplicitly]
        public static SharpAct operator |( EnumPipe<TOut> lhs, SharpAct<TOut> act ) {
            void Transformed( IEnumerable<TOut> _ ) {
                foreach (var item in lhs.Get) act.Action(item);
            }

            return SharpAct.FromAction( () => Transformed(null) );
        }*/

        /// <summary>
        /// Performs an action either on every item of the EnumPipe, or on the EnumPipe itself (depending on DoExecute function input).
        /// </summary>
        public static VOID operator |( EnumPipe<TOut> lhs, DoExecute doExecute ) {
            bool isOperatingOnEnumerable = typeof(IEnumerable<TOut>).IsAssignableFrom( doExecute.InType );

            if (isOperatingOnEnumerable)
                doExecute.Action(lhs.Get);
            else
                lhs.Get.ForEachAction( i => doExecute.Action(i) );
            
            return VOID.New;
        }
    }

    public struct DoExecute {
        [NotNull] internal readonly Action<object> Action;
        internal readonly Type InType;

        internal DoExecute( [NotNull] Action<object> action, Type inType = null ) {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            InType = inType;
        }

        internal static DoExecute FromAction<T>( Action<T> action ) => new DoExecute(i => action(i.To<T>()), typeof(T));
        internal static DoExecute FromAction( Action action )       => new DoExecute(_ => action());
    }
}
