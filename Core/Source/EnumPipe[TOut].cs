using JetBrains.Annotations;
using System.Collections.Generic;
using static SharpPipe.Commands;

namespace SharpPipe
{
	public partial struct EnumPipe<TOut> {
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		private EnumPipe( SharpFunc<IEnumerable<TOut>> func ) => Func = func;

		private SharpFunc<IEnumerable<TOut>> Func { get; }

		[NotNull] internal IEnumerable<TOut> Get => Func.NotNullFunc(default(IEnumerable<TOut>)).NotNull(typeof(EnumPipe<TOut>));

		private EnumPipe<T> TryCastTo<T>() {
			var convertedObj = Get.To<T>($"TryConvertTo : {this.T()}");

			return ENUM<T>.NEW | ADD | convertedObj;
		}
	}
}