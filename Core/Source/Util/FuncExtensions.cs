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
		public static Func<TIn, object> ToFunc<TIn>(this Action<TIn> action ) {
			object TempFunc(TIn obj ) {
				action(obj);
				return null;
			}

			return TempFunc;
		}

		/// <summary>
		/// Converts to a SharpFunc of a different type.
		/// </summary>
		public static SharpFunc<TOut> ToOut<TOut>(this ISharpFunc sharpFunc) => SharpFunc.FromFunc(sharpFunc.Func.To<object, TOut>());
		
		/// <summary>
		/// Converts to a SharpAct of a different type.
		/// </summary>
		public static SharpAct<TOut> ToOut<TOut>(this SharpAct<object> sharpAct) => SharpAct.FromAction<TOut>(i => sharpAct.Action(i));
	}
}