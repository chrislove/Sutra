using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public delegate object F( object obj );

	public delegate void A( object obj );

	public class GetPipe : PipeBase {
		protected GetPipe() {}
		protected GetPipe( bool isInitialized ) : base(isInitialized) {}

		public F Func { get; protected set; }

		private object Get() {
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");

			return Func(null);
		}


		public T Get<T>() {
			var pipeContent = Get();

			if (!(pipeContent is T))
				throw new InvalidOperationException($"Pipe content type is '{pipeContent.GetType()}', not '{typeof(T)}'");


			return pipeContent.To<T>();
		}
	}

	public sealed class EmptyPipe : GetPipe {}

	public sealed class GetPipe<TOut> : GetPipe {
		/// <summary>
		/// Returns a new pipe
		/// </summary>
		//[NotNull] public static GetPipe<TOut> FPipe(F func) => new GetPipe<TOut>(func);
		[NotNull] public static GetPipe<TOut> Empty => new GetPipe<TOut>();

		private GetPipe() {}

		public GetPipe( TOut obj ) : this(_ => obj) {}

		public GetPipe( [NotNull] Func<object, TOut> func ) : base(true) {
			if (func == null) throw new ArgumentNullException(nameof(func));
			Func = i => func(i);
		}

		public GetPipe( [NotNull] SharpFunc func ) : this(func.GetFunc<object, TOut>()) {}
		public GetPipe( [NotNull] SharpFunc<object> func ) : this(func.GetFunc<object, TOut>()) {}


		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |( EmptyPipe lhs, GetPipe<TOut> rhs ) {
			return rhs;
		}

		/*
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe operator |(GetPipe<TOut> lhs, GetPipe rhs) {
			return lhs | rhs.Func;
		}*/
		
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe operator |(GetPipe<TOut> lhs, ActPipe rhs)
		{
			return lhs | (p => rhs.Act(p) );
		}

	/*
	/// <summary>
	/// Forward pipe operator
	/// </summary>
	[NotNull]
	public static GetPipe operator |(GetPipe<TOut> lhs, Func<TOut, object> rhs) {
		var combined = lhs.Func + rhs.Sharp();

		return new GetPipe<TOut>(combined);
	}*/

	/*
	/// <summary>
	/// Forward pipe operator
	/// </summary>
	[NotNull]
	public static GetPipe operator |(GetPipe<TOut> lhs, F rhs) {
		var combined = lhs.Func + rhs.Sharp();

		return new GetPipe<TOut>(combined);
	}*/

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe operator |( GetPipe<TOut> lhs, A rhs ) {
			Action<object> combined = i => rhs(lhs.Func(i));
			return new ActPipe(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static TOut operator |( GetPipe<TOut> lhs, PipeEnd pipeEnd ) {
			return lhs.Get<TOut>();
		}
	}
}