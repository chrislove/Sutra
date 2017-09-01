using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class SharpAct {
		[NotNull]
		internal static SharpAct<T> FromAction<T>(Action<T> action) => new SharpAct<T>(action);
	}
}