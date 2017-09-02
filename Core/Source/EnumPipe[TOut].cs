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

		/// <summary>
		/// Concatenates pipe contents into a string
		/// </summary>
		[NotNull]
		public static string operator |( EnumPipe<TOut> lhs, DoConcat act ) {
			if (typeof(TOut) != typeof(string))
				throw new TypeMismatchException("CONCAT can only be done on EnumPipe<string>");
			
			return lhs.Get.Aggregate("", ( a, b ) => a + b + act.Separator);
		}

		/// <summary>
		/// Converts the pipe contents into List{TOut}
		/// </summary>
		[NotNull]
		public static List<TOut> operator |( EnumPipe<TOut> lhs, DoToList act ) => lhs.Get.ToList();

		/// <summary>
		/// Converts the pipe contents into TOut[]
		/// </summary>
		[NotNull]
		public static TOut[] operator |( EnumPipe<TOut> lhs, DoToArray act ) => lhs.Get.ToArray();

	}
}