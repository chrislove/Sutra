using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class EnumerablePipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static ActPipe<IEnumerable<TOut>> operator |([NotNull] EnumerablePipe<TOut> lhs, [NotNull] SharpAct<IEnumerable<TOut>> act)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (act == null) throw new ArgumentNullException(nameof(act));

			// Type validation not needed

			return ActPipe.FromAction<IEnumerable<TOut>>(_ => act.Action(lhs.Get));
		}

		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static ActPipe<IEnumerable<TOut>> operator |( [NotNull] EnumerablePipe<TOut> lhs, [NotNull] SharpAct<TOut> act ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (act == null) throw new ArgumentNullException(nameof(act));

			// Type validation not needed

			Action<IEnumerable<TOut>> transformed = _ => {
				                                        foreach (var item in lhs.Get) act.Action(item);
			                                        };

			return ActPipe.FromAction(transformed);
		}
	}
}