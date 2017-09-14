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
		internal readonly SeqOption<T> Option;

		internal static Seq<T> Empty => start<T>.seq | Enumerable.Empty<T>();

		internal Seq( SeqOption<T> option )  => Option = option;
		internal Seq( [CanBeNull] IEnumerable<Option<T>> enm ) => Option = enm.Return();
		internal Seq( [CanBeNull] IEnumerable<T> enm )         => Option = enm.Return().Return();

		internal static Seq<T> SkipSeq => new Seq<T>(SeqOption<T>.None);

		[Pure]
		internal Seq<TOut> Map<TOut>([NotNull] Func<IEnumerable<Option<T>>, IEnumerable<Option<TOut>>> func) => start<TOut>.seq | Option.Map(func);

		[Pure]
		internal Seq<TOut> Map<TOut>( [NotNull] Func<IEnumerable<IOption>, IEnumerable<Option<TOut>>> func ) => start<TOut>.seq | Option.Map(func);

		[Pure]
		internal Seq<TOut> Map<TOut>( [NotNull] Func<IEnumerable<T>, IEnumerable<TOut>> func ) => start<TOut>.seq | Option.Map(func);

		/// <summary>
		/// Appends a single object to sequence.
		/// </summary>
		[Pure] public static Seq<T> operator |(Seq<T> seq, [CanBeNull] T obj) {
			IEnumerable<T> Yield(T inobj) {
				yield return inobj;
			}

			return seq | Yield(obj);
		}

		/// <summary>
		/// Appends an enumerable to sequence.
		/// </summary>
		[Pure] public static Seq<T> operator |( Seq<T> seq, [CanBeNull] IEnumerable<T> enm ) {
			if (enm == null) return SkipSeq;

			foreach (var value in seq.Option)
				return start<T>.seq | value.Concat(enm.Return());

			return SkipSeq;
		}

		/// <summary>
		/// Appends an enumerable to sequence.
		/// </summary>
		[Pure] public static Seq<T> operator |(Seq<T> seq, [CanBeNull] IEnumerable<Option<T>> enm) {
			if (enm == null) return SkipSeq;
			
			foreach (var value in seq.Option)
				return start<T>.seq | value.Concat(enm);

			return SkipSeq;
		}

		/// <summary>
		/// Transforms sequence.
		/// </summary>
		/// <returns></returns>
		[Pure] public static Seq<T> operator |(Seq<T> seq, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func) => seq.Map(func);
		
		/// <summary>
		/// Transforms sequence.
		/// </summary>
		/// <returns></returns>
		[Pure] public static Seq<T> operator |(Seq<T> seq, [NotNull] Func<IEnumerable<IOption>, IEnumerable<Option<T>>> func) => seq.Map(func);

		/// <summary>
		/// Transforms sequence into a pipe.
		/// </summary>
		[Pure] public static Pipe<T> operator |(Seq<T> seq, [NotNull] Func<IEnumerable<Option<T>>, Option<T>> func) {
			foreach (var value in seq.Option)
				return start<T>.pipe | func(value);

			return Pipe<T>.SkipPipe;
		}

	}
}