using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public struct SharpAct<TIn> {
		internal SharpAct([NotNull] Action<TIn> act) => Action = act ?? throw new ArgumentNullException(nameof(act));

		[NotNull] internal readonly Action<TIn> Action;
		
		/// <summary>
		/// Executes the action.
		/// </summary>
		public static DoExecute operator ~( SharpAct<TIn> act ) {
			return DoExecute.FromAction( act.Action );
		}
	}
}