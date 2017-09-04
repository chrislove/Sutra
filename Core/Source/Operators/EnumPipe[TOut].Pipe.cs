using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct EnumPipe<TOut> {
		/// <summary>
		/// Returns SharpAct performing an action on every item of the EnumPipe.
		/// </summary>
		[UsedImplicitly]
		public static SharpAct operator |( EnumPipe<TOut> lhs, SharpAct<TOut> act ) {
			var enumerableAction = lhs.Get.ForEachAction( act.Action );

			return SharpAct.FromAction( enumerableAction );
		}

		/// <summary>
		/// Returns SharpAct performing an action on every item of the EnumPipe.
		/// </summary>
		public static SharpAct operator |( EnumPipe<TOut> lhs, SharpAct<object> rhs ) => lhs | rhs.ToOut<TOut>();
		
		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, Func<TOut, TOut> func ) {
			var enumerable = lhs.Get.Select(func);

			return EnumPipe.FromEnumerable(enumerable);
		}
		
		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, Func<IEnumerable<TOut>, IEnumerable<TOut>> func ) {
			var enumerable = func( lhs.Get );

			return EnumPipe.FromEnumerable(enumerable);
		}
	}
}