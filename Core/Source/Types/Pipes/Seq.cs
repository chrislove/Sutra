using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe
{
	/// <summary>
	/// A sequence of objects.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public partial struct Seq<T> : IPipe<T>//, IEnumerable<T>
	{
		[CanBeNull]
		private readonly IEnumerable<T> _contents;

		private readonly bool _shouldSkip;

		internal static Seq<T> Empty => start<T>.seq | Enumerable.Empty<T>();

		internal Seq([CanBeNull] IEnumerable<T> enm, bool shouldSkip = false) : this()
		{
			_contents = enm;
			_shouldSkip = shouldSkip || enm == null;
		}

		internal static Seq<T> SkipSeq => new Seq<T>(null, true);

		internal SeqOutput<T> Get => SeqOutput.New(_contents, _shouldSkip);

		internal Seq<TOut> Bind<TOut>([NotNull] Func<IEnumerable<T>, IEnumerable<TOut>> func)
		{
			var seqOut = this.Get;
			if (seqOut.ShouldSkip) return Seq<TOut>.SkipSeq;

			return start<TOut>.seq | func(seqOut.Contents);
		}


		/// <summary>
		/// Appends a single object to sequence.
		/// </summary>
		public static Seq<T> operator |(Seq<T> seq, [CanBeNull] T obj)
		{
			IEnumerable<T> Yield(T inobj)
			{
				yield return inobj;
			}

			return seq | Yield(obj);
		}

		/// <summary>
		/// Appends an enumerable to sequence.
		/// </summary>
		public static Seq<T> operator |(Seq<T> seq, [CanBeNull] IEnumerable<T> enm)
		{
			var seqOut = seq.Get;

			if (seqOut.ShouldSkip || enm == null) return Seq<T>.SkipSeq;

			return start<T>.seq | seqOut.Contents.Concat(enm);
		}

		/// <summary>
		/// Transforms sequence.
		/// </summary>
		/// <returns></returns>
		public static Seq<T> operator |(Seq<T> seq, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func) => seq.Bind(func);

		/// <summary>
		/// Transforms sequence into a pipe.
		/// </summary>
		public static Pipe<T> operator ^(Seq<T> seq, [NotNull] Func<IEnumerable<T>, T> func)
		{
			var seqOut = seq.Get;
			if (seqOut.ShouldSkip) return Pipe<T>.SkipPipe;

			return start<T>.pipe | func(seqOut.Contents);
		}
	}
}