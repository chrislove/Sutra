using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public class SharpAct<TIn> : SharpFunc<TIn, object> {
		[NotNull] internal Action<TIn> Action => i => base.Func(i);

		internal SharpAct([NotNull] Action<TIn> act) : base( act.ToFunc() ) {}

		/// <summary>
		/// Returns wrapped action.
		/// </summary>
		[NotNull]
		public static Action<TIn> operator ~(SharpAct<TIn> sharpFunc) => sharpFunc.Action;
	}
}