using System;
using JetBrains.Annotations;

namespace Tests {
	public delegate object F(object obj);
	public delegate void A(object obj);


	public struct PipeBase {
		private bool IsFunc => _func != null;
		private bool IsAct => _act != null;
		private bool IsInitialized => IsFunc || IsAct;

		private readonly F _func;

		private F Func {
			get {
				if (!IsFunc) throw new InvalidOperationException($"Pipe is not a function.");

				return _func;
			}
		}

		private readonly A _act;

		private A Act {
			get {
				if (!IsAct) throw new InvalidOperationException($"Pipe is not an action.");

				return _act;
			}
		}

		public object Get() {
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");
			if (!IsFunc) throw new InvalidOperationException("Calling wrong pipe method. Should Do() instead.");

			return Func(null);
		}

		public void Do() {
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");
			if (!IsAct) throw new InvalidOperationException("Calling wrong pipe method. Should Get() instead.");

			Act(null);
		}

		/// <summary>
		/// Returns a new pipe
		/// </summary>
		public static PipeBase P => new PipeBase();
		public static PipeBase APipe(A act)  => new PipeBase(act);
		public static PipeBase FPipe(F func) => new PipeBase(func);

		public static void Pipe(PipeBase pipeBase) => pipeBase.Do();
		public static T Pipe<T>(PipeBase pipeBase) => (T)pipeBase.Get();


		private PipeBase( [NotNull] F func ) {
			Console.WriteLine("New PipeBase func");

			if (func == null) throw new ArgumentNullException(nameof(func));
			_func = func;
			_act = null;
		}

		private PipeBase( [NotNull] A act ) {
			Console.WriteLine("New PipeBase act");

			if (act == null) throw new ArgumentNullException(nameof(act));
			_act = act;
			_func = null;
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeBase operator |( PipeBase lhs, PipeBase rhs ) {
			Console.WriteLine("pipe | pipe");

			if (rhs.IsFunc) {
				Func<object, object> combined = i => rhs.Func(lhs.Func(i));
				return FPipe(i => combined(i));
			} else {
				Action<object> combined = i => rhs.Act(lhs.Func(i));
				return APipe(i => combined(i));
			}
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeBase operator |( PipeBase lhs, F rhs ) {
			Console.WriteLine("pipe | F");

			Func<object, object> combined = i => rhs(lhs.Func(i));
			return FPipe(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeBase operator |( PipeBase lhs, A rhs ) {
			Console.WriteLine("pipe | A");

			Action<object> combined = i => rhs(lhs.Func(i));
			return APipe(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator. Initializes a pipe with object.
		/// </summary>
		public static PipeBase operator |( PipeBase lhs, object rhs ) {
			Console.WriteLine("pipe | obj");

			if (lhs.IsInitialized)
				throw new InvalidOperationException($"Pipe is already initialized, cannot initialize with object of type {rhs.GetType()}");

			return FPipe(i => rhs);
		}

		public static implicit operator PipeBase( F func ) {
			return new PipeBase(func);
		}

		public static implicit operator PipeBase( A act ) {
			return new PipeBase(act);
		}
	}
}