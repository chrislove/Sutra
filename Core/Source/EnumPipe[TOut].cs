using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public sealed partial class EnumPipe<TOut> : Pipe<IEnumerable<TOut>> {
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		internal EnumPipe([NotNull] IOutFunc<IEnumerable<TOut>> func) : base(func) {}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator |(EnumPipe<TOut> lhs, PipeEnd pipeEnd)
		{
			return lhs.Get;
		}

	}
}