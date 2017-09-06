using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
	public partial struct EnumPipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, [NotNull] Func<TOut, TOut> func ) {
			if (func == null) throw new ArgumentNullException(nameof(func));
			
			return lhs.Get.Select(func) | TO<TOut>.ENUM;
		}
		
		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, [NotNull] Func<IEnumerable<TOut>, IEnumerable<TOut>> func ) {
			if (func == null) throw new ArgumentNullException(nameof(func));
			
			return func(lhs.Get) | TO<TOut>.ENUM;
		}
	}
}