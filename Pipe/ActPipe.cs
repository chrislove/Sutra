using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class ActPipe {
		[NotNull] internal static ActPipe<T> FromAction<T>( [NotNull] Action<T> act )  => new ActPipe<T>(act);
		[NotNull] internal static ActPipe<T> FromAction<T>( [NotNull] SharpAct<T> act ) => new ActPipe<T>(act.Action);
	}
}