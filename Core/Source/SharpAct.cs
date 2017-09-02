using System;
using JetBrains.Annotations;

namespace SharpPipe {
	/// <summary>
	/// A wrapper around System.Action
	/// </summary>
	public sealed class SharpAct : SharpAct<object> {
		private SharpAct([NotNull] Action act) : base( _ => act() ) { }

		[NotNull] internal static SharpAct<T> FromAction<T>(Action<T> action) => new SharpAct<T>(action);

		[NotNull] internal static SharpAct FromAction(Action action) => new SharpAct(action);
		[NotNull] internal static SharpAct FromAction<T>(SharpAct<T> action) => new SharpAct( () => action.Action(default(T)) );
	}
}