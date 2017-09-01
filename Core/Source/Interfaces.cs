using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public interface IValidatableFunc {
		Type InType { get; }

		void ValidateInType( [CanBeNull] Type type );
	}

	public interface ISharpFunc : IValidatableFunc
	{
		Func<object, object> Func { get; }
	}

	public interface IOutFunc<out TOut> : ISharpFunc {
		new Func<object, TOut> Func { get; }
	}
}