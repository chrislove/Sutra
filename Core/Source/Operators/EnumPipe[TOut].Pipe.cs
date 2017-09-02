using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class EnumPipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static SharpAct<IEnumerable<TOut>> operator |([NotNull] EnumPipe<TOut> lhs, [NotNull] SharpAct<IEnumerable<TOut>> act)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (act == null) throw new ArgumentNullException(nameof(act));

			return SharpAct.FromAction<IEnumerable<TOut>>(_ => act.Action(lhs.Get));
		}

		/// <summary>
		/// Forward pipe operator. Performs an action on EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static SharpAct operator |( [NotNull] EnumPipe<TOut> lhs, [NotNull] SharpAct<TOut> act ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (act == null) throw new ArgumentNullException(nameof(act));

			void Transformed( IEnumerable<TOut> _ ) {
				foreach (var item in lhs.Get) act.Action(item);
			}

			return SharpAct.FromAction( () => Transformed(null) );
		}

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

		[NotNull]
		public static SharpAct operator |( EnumPipe<TOut> lhs, SharpAct<object> rhs ) {
			return lhs | rhs.ToOut<TOut>();
		}
	}
}