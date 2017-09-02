using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct EnumPipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static SharpAct<IEnumerable<TOut>> operator |(EnumPipe<TOut> lhs, [NotNull] SharpAct<IEnumerable<TOut>> act)
		{
			if (act == null) throw new ArgumentNullException(nameof(act));

			return SharpAct.FromAction<IEnumerable<TOut>>(_ => act.Action(lhs.Get));
		}

		/// <summary>
		/// Performs an action on every item of the EnumPipe
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static SharpAct operator |( EnumPipe<TOut> lhs, [NotNull] SharpAct<TOut> act ) {
			if (act == null) throw new ArgumentNullException(nameof(act));

			void Transformed( IEnumerable<TOut> _ ) {
				foreach (var item in lhs.Get) act.Action(item);
			}

			return SharpAct.FromAction( () => Transformed(null) );
		}

		/// <summary>
		/// Performs an action on every item of the EnumPipe
		/// </summary>
		[NotNull]
		public static SharpAct operator |( EnumPipe<TOut> lhs, SharpAct<object> rhs ) {
			return lhs | rhs.ToOut<TOut>();
		}
	}
}