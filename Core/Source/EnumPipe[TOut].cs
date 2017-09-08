using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public partial struct EnumPipe<TOut> : IPipe<TOut> {
		internal static EnumPipe<TOut> Empty => new EnumPipe<TOut>( Enumerable.Empty<TOut>() );
		
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		private EnumPipe( SharpFunc<IEnumerable<TOut>> func ) => Func = func;

		private SharpFunc<IEnumerable<TOut>> Func { get; }

		[NotNull] internal IEnumerable<TOut> Get => Func.NotNullFunc(default(IEnumerable<TOut>)).NotNull(typeof(EnumPipe<TOut>));
	}
}