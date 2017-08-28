using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class PipeEnd<T> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static T operator |(GetPipe lhs, PipeEnd<T> rhs) {
			var pipeContent = lhs.Get();

			if ( !(pipeContent is T) )
				throw new InvalidOperationException($"Pipe content type is '{pipeContent.GetType()}', not '{typeof(T)}'");

			return (T)pipeContent;
		}
	}
}