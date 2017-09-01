using System;
using JetBrains.Annotations;

namespace SharpPipe {
	/*
	public interface IGetPipe {
		OutFunc<object> Func { get; }
	}*/

	public interface IValidatableFunc {
		Type InType { get; }
		Type OutType { get; }

		void ValidateInType( [CanBeNull] Type type );
		void ValidateOutType( [CanBeNull] Type type );
		void ValidateCompatibilityWith( ISharpFunc rhsFunc );
	}

	public interface ISharpFunc : IValidatableFunc
	{
		Func<object, object> Func { get; }
	}


	public interface IInFunc<in TIn> : ISharpFunc {
		new Func<TIn, object> Func { get; }
	}


	public interface IOutFunc<out TOut> : ISharpFunc {
		new Func<object, TOut> Func { get; }
	}
}