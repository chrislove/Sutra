using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe
{
    [PublicAPI]
    public static partial class Commands
    {
        /// <summary>
        /// Returns pipe or sequence contents. Safe, with unsafe variations.
        /// </summary>
        /// <example><code>
        /// [safe]   pipe | get
        /// [unsafe] pipe | !get
        /// [unsafe] seq  | !!get
        /// </code></example>
        public static DoGet get => new DoGet();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoGet
    {
        /// <summary>
        /// For Pipe: returns actual value, unsafe.
        /// For Seq:  returns Option{IEnumerable{T}}, safe.
        /// </summary>
        /// <exception cref="EmptyPipeException"></exception>
        /// <exception cref="EmptySequenceException"></exception>
        public static DoGet1 operator !( DoGet _ ) => new DoGet1();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    /// <exception cref="EmptyPipeException"></exception>
    public struct DoGet1
    {
        /// <summary>
        /// Returns Option{IEnumerable{T}}, safe.
        /// </summary>
        public static DoGet2 operator !( DoGet1 _ ) => new DoGet2();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoGet2
    {
        /// <summary>
        /// Returns IEnumerable{T}, unsafe.
        /// </summary>
        public static DoGet3 operator !( DoGet2 _ ) => new DoGet3();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoGet3 { }


    partial struct Seq<T>
    {
        /// <summary>
        /// Returns sequence contents. Safe.
        /// </summary>
        public static SeqOption<T> operator |( Seq<T> seq, DoGet _ ) => seq.Option;

        /// <summary>
        /// Returns sequence contents if all are non-empty, otherwise none. Safe.
        /// </summary>
        public static Option<IEnumerable<T>> operator |( Seq<T> seq, DoGet1 _ ) => seq.Option.Lower();

        /// <summary>
        /// Returns sequence contents. Unsafe.
        /// </summary>
        /// <exception cref="EmptySequenceException">Thrown when the entire sequence is none.</exception>
        [NotNull]
        public static IEnumerable<Option<T>> operator |( Seq<T> seq, DoGet2 _ ) => seq.Option.ValueOrFail(EmptySequenceException.For<T>);

        /// <summary>
        /// Returns sequence contents. Unsafe.
        /// </summary>
        /// <exception cref="EmptySequenceException">Thrown when any item within the sequence is none.</exception>
        [NotNull]
        public static IEnumerable<T> operator |( Seq<T> seq, DoGet3 _ ) => (seq | !!get).Select( o => o.ValueOrFail(EmptySequenceException.For<T>) );
    }

    public partial struct Pipe<T>
    {
        /// <summary>
        /// Returns pipe contents. Unsafe.
        /// </summary>
        /// <exception cref="EmptyPipeException"></exception>
        [NotNull]
        public static T operator |( Pipe<T> pipe, DoGet1 _ ) => pipe.Option.ValueOrFail(EmptyPipeException.For<T>);

        /// <summary>
        /// Returns pipe contents.
        /// </summary>
        public static Option<T> operator |( Pipe<T> pipe, DoGet _ ) => pipe.Option;
    }
}