using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class EnumerablePipe<TOut> : GetPipe<IEnumerable<TOut>> {
		internal EnumerablePipe([CanBeNull] IEnumerable<TOut> obj) : this( OutFunc.WithValue(obj) ) { }

		internal EnumerablePipe([NotNull] IOutFunc<IEnumerable<TOut>> func) : base(func) {}

		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		[NotNull]
		public static EnumerablePipe<TOut> operator +([NotNull] EnumerablePipe<TOut> lhs, [NotNull] EnumerablePipe<TOut> rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			var combined = lhs.Get.Concat( rhs.Get );

			return EnumerablePipe.FromEnumerable(combined);
		}

		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		[NotNull]
		public static EnumerablePipe<TOut> operator +([NotNull] EnumerablePipe<TOut> lhs, [NotNull] IEnumerable<TOut> rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return lhs + EnumerablePipe.FromEnumerable(rhs);
		}

		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static ActPipe< IEnumerable<TOut> > operator |( [NotNull] EnumerablePipe<TOut> lhs, [NotNull] SharpAct<IEnumerable<TOut>> act) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (act == null) throw new ArgumentNullException(nameof(act));

			// Type validation not needed

			return ActPipe.FromAction<IEnumerable<TOut>>( _ => act.Action(lhs.Get) );
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