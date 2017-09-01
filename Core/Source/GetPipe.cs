using System;
using JetBrains.Annotations;

namespace SharpPipe
{
	public abstract class GetPipe : PipeBase
	{
		[NotNull] internal OutFunc<object> Func { get; }

		protected GetPipe( [NotNull] ISharpFunc func, Type outType = null ) {
			if (func == null) throw new ArgumentNullException(nameof(func));

			Func = func.ToOut<object>();

			OutType = outType;
		}

		/// <summary>
		/// Used for validation, in case the in type is known.
		/// </summary>
		internal readonly Type OutType;

		internal void ValidateCompatibilityWith(ISharpFunc rhsFunc)
		{
			Console.WriteLine($"ValidateCompatibilityWith {GetType()} with {rhsFunc.GetType()}");

			rhsFunc.ValidateInType(OutType);
			this.ValidateOutType(rhsFunc.InType);
		}

		internal void ValidateOutType(Type type){
			if (OutType == null || type == null) return;

			if (OutType != type)
				throw new TypeMismatchException(GetType(), type, OutType, "Out");
		}



		[NotNull] internal static GetPipe<T> FromObject<T>(T obj) => FromFunc(OutFunc.WithValue(obj));
		[NotNull] internal static GetPipe<T> FromFunc<T>(IOutFunc<T> func) => new GetPipe<T>(func);
	}
}