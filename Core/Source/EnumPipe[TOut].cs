using System;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public partial struct EnumPipe<TOut> : IPipe {
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		internal EnumPipe( [NotNull] IOutFunc<IEnumerable<TOut>> func ) {
			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<IEnumerable<TOut>>();
		}
		
		internal SharpFunc<IEnumerable<TOut>> Func { get; }
		SharpFunc<object> IPipe.Func => SharpFunc.FromFunc<object>(Func);


		[CanBeNull] internal IEnumerable<TOut> Get => Func.Func(default(IEnumerable<TOut>));

		/// <summary>
		/// Pipe decomposition operator.
		/// Returns the value contained within the pipe. An equivalent of: pipe | OUT
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator ~( EnumPipe<TOut> pipe ) => pipe | Pipe.OUT;

		/// <summary>
		/// Returns pipe contents
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator |(EnumPipe<TOut> lhs, DoEnd act) => lhs.Get;
	}
}