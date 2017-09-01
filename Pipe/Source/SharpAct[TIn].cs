using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public sealed class SharpAct<TIn> : SharpFunc<TIn, object> {
		[NotNull] internal Action<TIn> Action => i => base.Func(i);

		internal SharpAct([NotNull] Action<TIn> act) : base( act.ToFunc() ) {}

		[NotNull]
		public static ActPipe<TIn> operator |(GetPipe<TIn> lhs, SharpAct<TIn> rhs)
		{
			// Type validation not needed

			return lhs | (p => rhs.Action(p));
		}
	}
}