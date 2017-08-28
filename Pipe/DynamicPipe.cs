using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	//public delegate dynamic F(dynamic obj);
	//public delegate void A(dynamic obj);
	
	public struct PipeDynamic
	{
		private bool IsFunc => _func != null;
		private bool IsAct  => _act != null;
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

		public dynamic Get()
		{
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");
			if (!IsFunc) throw new InvalidOperationException("Calling wrong pipe method. Should Do() instead.");

			return Func(null);
		}

		public void Do()
		{
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");
			if (!IsAct) throw new InvalidOperationException("Calling wrong pipe method. Should Get() instead.");

			Act(null);
		}

		/// <summary>
		/// Returns a new pipe
		/// </summary>
		public static PipeDynamic Pipe => new PipeDynamic();

		private PipeDynamic([NotNull] F func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			_func = func;
			_act = null;
		}

		private PipeDynamic([NotNull] A act)
		{
			if (act == null) throw new ArgumentNullException(nameof(act));
			_act = act;
			_func = null;
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeDynamic operator |(PipeDynamic lhs, PipeDynamic rhs)
		{
			if (rhs.IsFunc) {
				Func<object, object> combined = i => rhs.Func(lhs.Func(i));
				return new PipeDynamic(i => combined(i));
			} else {
				Action<object> combined = i => rhs.Act(lhs.Func(i));
				return new PipeDynamic(i => combined(i));
			}
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeDynamic operator |(PipeDynamic lhs, F rhs)
		{
			Func<object, object> combined = i => rhs(lhs.Func(i));
			return new PipeDynamic( (F) (i => combined(i)) );
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeDynamic operator |(PipeDynamic lhs, A rhs)
		{
			Action<object> combined = i => rhs(lhs.Func(i));
			return new PipeDynamic( (A) (i => combined(i)) );
		}
		
		/// <summary>
		/// Forward pipe operator. Initializes a pipe with object.
		/// </summary>
		public static PipeDynamic operator |(PipeDynamic lhs, object rhs)
		{
			if (lhs.IsInitialized)
				throw new InvalidOperationException($"Pipe is already initialized, cannot initialize with object of type {rhs.GetType()}");

			return new PipeDynamic(i => rhs);
		}

		public static implicit operator PipeDynamic(F func)
		{
			return new PipeDynamic(func);
		}

		public static implicit operator PipeDynamic(A act)
		{
			return new PipeDynamic(act);
		}
	}
	/*
	public sealed class DynamicPipe : DynamicObject {
		private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			return _properties.TryGetValue(binder.Name, out result);
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_properties[binder.Name] = value;
			return true;
		}

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

		public object Get()
		{
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");
			if (!IsFunc) throw new InvalidOperationException("Calling wrong pipe method. Should Do() instead.");

			return Func(null);
		}

		public void Do()
		{
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");
			if (!IsAct) throw new InvalidOperationException("Calling wrong pipe method. Should Get() instead.");

			Act(null);
		}

		/// <summary>
		/// Returns a new pipe
		/// </summary>
		public static PipeBase Pipe => new PipeBase();

		private PipeBase([NotNull] F func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			_func = func;
			_act = null;
		}

		private PipeBase([NotNull] A act)
		{
			if (act == null) throw new ArgumentNullException(nameof(act));
			_act = act;
			_func = null;
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeBase operator |(PipeBase lhs, PipeBase rhs)
		{
			if (rhs.IsFunc) {
				Func<object, object> combined = i => rhs.Func(lhs.Func(i));
				return new PipeBase(i => combined(i));
			} else {
				Action<object> combined = i => rhs.Act(lhs.Func(i));
				return new PipeBase(i => combined(i));
			}
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeBase operator |(PipeBase lhs, F rhs)
		{
			Func<object, object> combined = i => rhs(lhs.Func(i));
			return new PipeBase(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static PipeBase operator |(PipeBase lhs, A rhs)
		{
			Action<object> combined = i => rhs(lhs.Func(i));
			return new PipeBase(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator. Initializes a pipe with object.
		/// </summary>
		public static PipeBase operator |(PipeBase lhs, object rhs)
		{
			if (lhs.IsInitialized)
				throw new InvalidOperationException($"Pipe is already initialized, cannot initialize with object of type {rhs.GetType()}");

			return new PipeBase(i => rhs);
		}

		public static implicit operator PipeBase(F func)
		{
			return new PipeBase(func);
		}

		public static implicit operator PipeBase(A act)
		{
			return new PipeBase(act);
		}
	}
	*/
}