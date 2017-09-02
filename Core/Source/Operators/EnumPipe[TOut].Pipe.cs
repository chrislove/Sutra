using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
	public partial class EnumPipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static SharpAct<IEnumerable<TOut>> operator |([NotNull] EnumPipe<TOut> lhs, [NotNull] SharpAct<IEnumerable<TOut>> act)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (act == null) throw new ArgumentNullException(nameof(act));

			return SharpAct.FromAction<IEnumerable<TOut>>(_ => act.Action(lhs.Get));
		}

		/// <summary>
		/// Performs an action on every item of the EnumPipe
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static SharpAct operator |( [NotNull] EnumPipe<TOut> lhs, [NotNull] SharpAct<TOut> act ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
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