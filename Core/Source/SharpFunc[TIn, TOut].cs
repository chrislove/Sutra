using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public partial struct SharpFunc<TIn, TOut> : IOutFunc<TOut> {
		[NotNull] public Func<TIn, TOut> Func { get; }

		internal SharpFunc([NotNull] Func<TIn, TOut> func) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}

		Func<object, object> ISharpFunc.Func {
			get {
				var @this = this;
				
				return i => @this.Func(i.To<TIn>());
			}
		}

		Func<object, TOut> IOutFunc<TOut>.Func {
			get {
				var @this = this;
				
				return i => @this.Func(i.To<TIn>());
			}
		}
	}
}