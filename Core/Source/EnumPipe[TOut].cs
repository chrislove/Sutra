using System;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public partial struct EnumPipe<TOut> {
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		internal EnumPipe( SharpFunc<IEnumerable<TOut>> func ) => Func = func;

		internal SharpFunc<IEnumerable<TOut>> Func { get; }

		[CanBeNull] internal IEnumerable<TOut> Get => Func.Func(default(IEnumerable<TOut>));

		/// <summary>
		/// Pipe decomposition operator.
		/// Returns the value contained within the pipe. An equivalent of: pipe | OUT
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator ~( EnumPipe<TOut> pipe ) => pipe - Commands.OUT;

		/// <summary>
		/// Returns pipe contents
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator -(EnumPipe<TOut> lhs, DoEnd act) => lhs.Get;
	}
}