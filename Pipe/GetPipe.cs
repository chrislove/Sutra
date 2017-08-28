using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public delegate object F(object obj);
	public delegate void   A(object obj);

	public sealed class GetPipe : PipeBase {
		private F Func { get; }

		public object Get() {
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");

			return Func(null);
		}
		

		/// <summary>
		/// Returns a new pipe
		/// </summary>
		[NotNull] private static GetPipe FPipe(F func) => new GetPipe(func);

		[NotNull] public static GetPipe Empty => new GetPipe();

		private GetPipe() {}

		private GetPipe( [NotNull] F func ) : base(true) {
			if (func == null) throw new ArgumentNullException(nameof(func));
			Func = func;
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static PipeBase operator |(GetPipe lhs, PipeBase rhs) {
			if (rhs is GetPipe) return lhs | (GetPipe) rhs;
			if (rhs is ActPipe) return lhs | (ActPipe) rhs;

			throw new InvalidOperationException($"Pipe {rhs.GetType()} is neither GetPipe or ActPipe");
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe operator |(GetPipe lhs, GetPipe rhs) {
			return lhs | rhs.Func;
		}
		
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe operator |(GetPipe lhs, ActPipe rhs)
		{
			return lhs | (p => rhs.Act(p) );
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe operator |(GetPipe lhs, F rhs)
		{
			Func<object, object> combined = i => rhs(lhs.Func(i));
			return GetPipe.FPipe(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe operator |(GetPipe lhs, A rhs)
		{
			Action<object> combined = i => rhs(lhs.Func(i));
			return ActPipe.APipe(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator. Initializes a pipe with object.
		/// </summary>
		[NotNull]
		public static GetPipe operator |(GetPipe lhs, object rhs)
		{
			if (lhs.IsInitialized)
				throw new InvalidOperationException($"Pipe is already initialized, cannot initialize with object of type {rhs.GetType()}");

			return GetPipe.FPipe(i => rhs);
		}

		[NotNull]
		public static implicit operator GetPipe( F func ) {
			return new GetPipe(func);
		}
	}
}