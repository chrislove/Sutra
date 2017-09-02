using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct EnumPipe<TOut> {
		/// <summary>
		/// Returns SharpAct performing an action on EnumPipe.
		/// </summary>
		[UsedImplicitly]
		public static SharpAct<IEnumerable<TOut>> operator |(EnumPipe<TOut> lhs, SharpAct<IEnumerable<TOut>> act)
			=> SharpAct.FromAction<IEnumerable<TOut>>(_ => act.Action(lhs.Get));

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
	}
}