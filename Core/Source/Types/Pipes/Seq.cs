using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe
{
	/// <summary>
	/// A sequence monad containing a set of objects.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public partial struct Seq<T> : IPipe<T>//, IEnumerable<T>
	{
		internal readonly Option<IEnumerable<T>> Option;

		internal static Seq<T> Empty => start<T>.seq | Enumerable.Empty<T>();

		internal Seq( Option<IEnumerable<T>> option )  => Option = option;
		internal Seq( [CanBeNull] IEnumerable<T> enm ) => Option = enm.ToOption();

		internal static Seq<T> SkipSeq => new Seq<T>(Option<IEnumerable<T>>.None);
		
		internal Seq<TOut> Bind<TOut>([NotNull] Func<IEnumerable<T>, IEnumerable<TOut>> func)
		{
			foreach (var value in Option)
				return start<TOut>.seq | func(value);

			return Seq<TOut>.SkipSeq;
		}

		/// <summary>
		/// Appends a single object to sequence.
		/// </summary>
		public static Seq<T> operator |(Seq<T> seq, [CanBeNull] T obj) {
			IEnumerable<T> Yield(T inobj) {
				yield return inobj;
			}

			return seq | Yield(obj);
		}

		/// <summary>
		/// Appends an enumerable to sequence.
		/// </summary>
		public static Seq<T> operator |(Seq<T> seq, [CanBeNull] IEnumerable<T> enm) {
			if (enm == null)
				return SkipSeq;
			
			foreach (var value in seq.Option)
				return start<T>.seq | value.Concat(enm).ToOption();

			return SkipSeq;
		}

		/// <summary>
		/// Transforms sequence.
		/// </summary>
		/// <returns></returns>
		public static Seq<T> operator |(Seq<T> seq, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func) => seq.Bind(func);

		/// <summary>
		/// Transforms sequence into a pipe.
		/// </summary>
		public static Pipe<T> operator ^(Seq<T> seq, [NotNull] Func<IEnumerable<T>, T> func) {
			foreach (var value in seq.Option)
				return start<T>.pipe | func(value);

			return Pipe<T>.SkipPipe;
		}
		
		public static implicit operator Seq<T>(Option<IEnumerable<T>> option) => new Seq<T>(option);

	}
}