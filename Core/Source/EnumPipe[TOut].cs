using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace SharpPipe
{
	public partial class EnumPipe<TOut> : Pipe<IEnumerable<TOut>> {
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		internal EnumPipe([NotNull] IOutFunc<IEnumerable<TOut>> func) : base(func) {}

		/// <summary>
		/// Returns pipe contents
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator |(EnumPipe<TOut> lhs, DoEnd act) => lhs.Get;
	}
}