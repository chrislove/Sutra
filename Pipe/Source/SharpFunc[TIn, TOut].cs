using JetBrains.Annotations;
using System;
using System.Linq;

namespace SharpPipe
{
	public class SharpFunc<TIn, TOut> : SharpFunc, IInFunc<TIn>, IOutFunc<TOut> {
		[NotNull] internal new Func<TIn, TOut> Func => base.Func.To<TIn, TOut>();

		Func<object, object> ISharpFunc.Func        => base.Func;
		Func<TIn, object> IInFunc<TIn>.Func         => base.Func.ToIn<TIn>();
		Func<object, TOut> IOutFunc<TOut>.Func      => base.Func.ToOut<TOut>();

		internal SharpFunc( [NotNull] Func<TIn, TOut> func, Type inType = null, Type outType = null ) :
										base(i => func(i.To<TIn>()), inType, outType) { }

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<TIn, TOut> operator +( [NotNull] IOutFunc<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			Func<object, TIn> lhsFunc    = lhs.Func;
			Func<TIn, TOut>   rhsFunc    = rhs.Func;

			TOut CombinedFunc( TIn i ) => rhsFunc(lhsFunc(i));

			return SharpFunc.FromFunc<TIn, TOut>(CombinedFunc);
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static OutFunc<TOut> operator +([NotNull] Func<object, TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			TOut CombinedFunc(object i) => rhs.Func(lhs(i));

			return OutFunc.FromFunc(CombinedFunc);
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<TIn> operator +([NotNull] SharpFunc<TIn, TOut> lhs, [NotNull] Action<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			void Combined(TIn obj) => rhs( lhs.Func(obj) );

			return SharpAct.FromAction<TIn>(Combined);
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |([NotNull] GetPipe<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return GetPipe.FromFunc(lhs.Func + rhs);
		}


		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static EnumerablePipe<TOut> operator |([NotNull] EnumerablePipe<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> func)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (func == null) throw new ArgumentNullException(nameof(func));

			// Type validation not needed

			var enumerable = lhs.Get.Select(i => func.Func(i).To<TOut>());

			return EnumerablePipe.FromEnumerable(enumerable);
		}
	}
}