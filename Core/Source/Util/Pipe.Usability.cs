using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
	public static partial class Pipe {
		[NotNull] public static SharpAct<object> WriteLine => _(( object i ) => Console.WriteLine(i));
		[NotNull] public static SharpFunc<string, TOut> _<TOut>([CanBeNull] Func<string, TOut> func) => SharpFunc.FromFunc(func);
	}
}