using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public class SharpFunc {
		public SharpFunc( Func<object, object> func ) {
			_func = func;
		}

		public SharpFunc(object value) : this(i => value) {	}

		private readonly Func<object, object> _func;

		public Func<object, object> GetFunc() => i => _func(i);
		public Func<TIn, object> GetFunc<TIn>() => i => _func(i);
		public Func<TIn, TOut> GetFunc<TIn, TOut>() => i => _func(i).To<TOut>();

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc operator +(F lhs, SharpFunc rhs) {
			Func<object, object> lhsFunc = i => lhs(i);
			Func<object, object> rhsFunc = rhs.GetFunc();

			Func<object, object> combined = i => rhsFunc(lhsFunc(i));

			return new SharpFunc(combined);
		}


		[NotNull]
		public static explicit operator SharpFunc(Func<object, object> func)
												=> new SharpFunc(func);

		[NotNull]
		public static explicit operator SharpFunc(F func)
												=> new SharpFunc(func);
	}

	public class SharpFunc<TIn> : SharpFunc {
		private readonly Func<TIn, object> _func;

		public new Func<TIn, object> GetFunc() => _func;


		public SharpFunc( Func<TIn, object> func ) : base(i => func(i.To<TIn>())) {
			_func = func;
		}

		public SharpFunc(TIn value) : this(i => value) { }

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		/*[NotNull]
		public static GetPipe<TIn> operator |(GetPipe lhs, SharpFunc<TIn> rhs) {
			var combined = lhs.Func + rhs;

			return new GetPipe<TIn>(combined);
		}*/


		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object> operator +(SharpFunc lhs, SharpFunc<TIn> rhs)
		{
			Func<object, TIn> lhsFunc = lhs.GetFunc<object, TIn>();
			Func<TIn, object> rhsFunc = rhs.GetFunc();

			Func<object, object> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object>(combined);
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object> operator +(F lhs, SharpFunc<TIn> rhs)
		{
			Func<object, TIn> lhsFunc = i => lhs(i).To<TIn>();
			Func<TIn, object> rhsFunc = rhs.GetFunc();

			Func<object, object> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object>(combined);
		}


		[NotNull]
		public static explicit operator SharpFunc<TIn>(Func<object, object> func)
									=> new SharpFunc<TIn>(i => func(i));
		[NotNull]
		public static explicit operator SharpFunc<TIn>(F func)
									=> new SharpFunc<TIn>(i => func(i));

		[NotNull]
		public static explicit operator SharpFunc<TIn>(Func<TIn, object> func)
											=> new SharpFunc<TIn>( func);

		[NotNull]
		public static explicit operator Func<TIn, object>(SharpFunc<TIn> sharpFunc) => sharpFunc.GetFunc();
	}

	public sealed class SharpFunc<TIn, TOut> : SharpFunc<TIn> {
		private readonly Func<TIn, TOut> _func;

		public new Func<TIn, TOut> GetFunc() => _func;

		public SharpFunc( Func<TIn, TOut> func ) : base(i => func(i)) {
			_func = func;
		}
		public SharpFunc(TOut value) : this(i => value) { }

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object, TOut> operator +(SharpFunc lhs, SharpFunc<TIn, TOut> rhs ) {
			Func<object, TIn> lhsFunc    = lhs.GetFunc<object, TIn>();
			Func<TIn, TOut>      rhsFunc = rhs.GetFunc();

			Func<object, TOut> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object, TOut>(combined);
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object, TOut> operator +(F lhs, SharpFunc<TIn, TOut> rhs ) {
			Func<object, TIn> lhsFunc    = i => lhs(i).To<TIn>();
			Func<TIn, TOut>      rhsFunc = rhs.GetFunc();

			Func<object, TOut> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object, TOut>(combined);
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |(GetPipe lhs, SharpFunc<TIn, TOut> rhs) {
			var combined = lhs.Func + rhs;

			return new GetPipe<TOut>(combined.GetFunc<object, TOut>());
		}

		[NotNull] public static explicit operator SharpFunc<TIn, TOut>(Func<object, object> func)
											=> new SharpFunc<TIn, TOut>(i => func(i).To<TOut>());

		[NotNull] public static explicit operator SharpFunc<TIn, TOut>(F func)
											=> new SharpFunc<TIn, TOut>(i => func(i).To<TOut>());

		[NotNull] public static explicit operator SharpFunc<TIn, TOut>(Func<TIn, TOut> func)	
											=> new SharpFunc<TIn, TOut>(func);

		[NotNull]
		public static explicit operator Func<TIn, TOut>( SharpFunc<TIn, TOut> sharpFunc ) => sharpFunc.GetFunc();
	}
}