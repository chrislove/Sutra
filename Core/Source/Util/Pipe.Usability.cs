using System;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
	public static partial class Pipe {
		public static SharpAct<object> WriteLine => _( (object i) => Console.WriteLine(i) );
		public static SharpFunc<string, TOut> _<TOut>([CanBeNull] Func<string, TOut> func) => SharpFunc.FromFunc(func);
	}
}