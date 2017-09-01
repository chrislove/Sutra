using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static partial class PipeUtil {
		[NotNull] public static SharpAct<object> WriteLine => __(( object i ) => Console.WriteLine(i));
	}
}