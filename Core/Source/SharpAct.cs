using System;
using JetBrains.Annotations;

namespace SharpPipe {
	/// <summary>
	/// A wrapper around System.Action
	/// </summary>
	public partial struct SharpAct {
		internal SharpAct([NotNull] Action act) => Action = act ?? throw new ArgumentNullException(nameof(act));

		[NotNull] internal readonly Action Action;

		internal static SharpAct<T> FromAction<T>(Action<T> action) => new SharpAct<T>(action);

		internal static SharpAct FromAction(Action action) => new SharpAct(action);
		internal static SharpAct FromAction<T>(SharpAct<T> action) => new SharpAct( () => action.Action(default(T)) );
		
		/// <summary>
		/// Executes the action
		/// </summary>
		public static DoExecute operator ~( SharpAct act ) {
			return DoExecute.FromAction(act.Action);
		}
	}
}