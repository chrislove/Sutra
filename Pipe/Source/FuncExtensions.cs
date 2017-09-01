using System;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class FuncExtensions {
		[NotNull]
		public static Action<TOut> CombineWith<TIn, TOut>( this Func<TIn, TOut> func, Action<TOut> action )
															=> i => action(
																		   func( i.To<TIn>() )
																		  );

		[NotNull]
		public static Func<TIn, TOut> To<TIn, TOut>( this Func<object, object> func )
									=> i => func(i).To<TOut>();

		[NotNull]
		public static Func<object, TOut> ToOut<TOut>( this Func<object, object> func )
									=> i => func(i).To<TOut>();

		[NotNull]
		public static Func<TIn, object> ToIn<TIn>( this Func<object, object> func )
									=> i => func(i);

		[NotNull]
		public static Func<TIn, object> ToFunc<TIn>(this Action<TIn> action ) {
			object TempFunc(TIn obj ) {
				action(obj);
				return null;
			}

			return TempFunc;
		}


		/// <summary>
		/// Converts to a SharpFunc of a different type
		/// </summary>
		[NotNull]
		public static SharpFunc<TIn, TOut> To<TIn, TOut>(this ISharpFunc sharpFunc) => SharpFunc.FromFunc(sharpFunc.Func.To<TIn, TOut>());

		/// <summary>
		/// Converts to a SharpFunc of a different type.
		/// </summary>
		[NotNull]
		public static OutFunc<TOut> ToOut<TOut>(this ISharpFunc sharpFunc) => OutFunc.FromFunc(sharpFunc.Func.To<object, TOut>());
	}
}