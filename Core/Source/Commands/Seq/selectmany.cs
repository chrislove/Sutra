using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <example>
        /// <code>
        /// pipe | selectmany | (i => Enumerable.Repeat(i, 3))
        /// </code>
        /// </example>
        public static DoSelectMany selectmany => new DoSelectMany();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectMany { }
    
    public partial struct Seq<T> {
        public static DoSelectMany<T> operator |( Seq<T> pipe, DoSelectMany doSelect ) => new DoSelectMany<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectMany<T> {
        private readonly Seq<T> pipe;

        internal DoSelectMany( Seq<T> pipe ) => this.pipe = pipe;

        public static Seq<T> operator |( DoSelectMany<T> doSelect, [NotNull] Func<T, IEnumerable<T>> func )
            => doSelect.pipe.Get.SelectMany(func) | to<T>.pipe;
    }
}