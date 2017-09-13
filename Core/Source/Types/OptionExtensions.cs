using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class OptionExtensions {
        public static Option<T>    ToOption<T>              ([CanBeNull] this T obj) => new Option<T>(obj);
        public static EnmOption<T> ToOption<T> ([CanBeNull] this IEnumerable<Option<T>> enm) => new EnmOption<T>(enm);

        /// <summary>
        /// Lifts every value of the enumerable to Option{T}
        /// </summary>
        public static EnmOption<T> ToEnmOption<T>( [CanBeNull] this IEnumerable<T> enm ) => new EnmOption<T>(enm.Lift());

        
        /// <summary>
        /// Lifts every value of the enumerable to Option{T}
        /// </summary>
        public static IEnumerable<Option<T>> Lift<T>( [CanBeNull] this IEnumerable<T> enm ) => enm?.Select(i => i.ToOption());
        
        [NotNull]
        public static Func<Option<T>, Option<U>> Lift<T, U>( this Func<T, U> func ) => i => func(i.ValueOrFail()).ToOption();

        public static Func<EnmOption<T>, Option<U>> Lift<T, U>( this Func<IEnumerable<T>, U> func ) => i => func(i.Lower()).ToOption();
        public static Func<Option<T>, EnmOption<U>> Lift<T, U>( this Func<T, IEnumerable<U>> func ) => i => func(i.ValueOrFail()).ToEnmOption();

        [NotNull]
        public static Func<Option<T>, U> LiftIn<T, U>( this Func<T, U> func ) => i => func(i.ValueOrFail());

        [NotNull]
        public static Func<EnmOption<T>, U> LiftIn<T, U>( this Func<IEnumerable<T>, U> func )
                        => i => func(i.Lower());
        [NotNull]
        public static Func<EnmOption<T>, EnmOption<U>> Lift<T, U>( this Func<IEnumerable<T>, IEnumerable<U>> func )
                        => i => func(i.Lower()).Lift().ToOption();

        
        /// <summary>
        /// Lowers every value of the enumerable to T
        /// </summary>
        public static IEnumerable<T> Lower<T>( this EnmOption<T> option ) => option.ValueOrFail().Lower();
        
        /// <summary>
        /// Lowers every value of the enumerable to T
        /// </summary>
        public static IEnumerable<T> Lower<T>( this IEnumerable<Option<T>> enm ) => enm.Select(i => i.ValueOrFail());

        [NotNull]
        public static Func<T, U> Lower<T, U>( this Func<Option<T>, Option<U>> func ) => i => func(i.ToOption()).ValueOrFail();

        [NotNull]
        public static Func<IEnumerable<T>, U> Lower<T, U>( this Func<EnmOption<T>, Option<U>> func ) => i => func(i.Lift().ToOption()).ValueOrFail();

        [NotNull]
        public static Func<T, IEnumerable<U>> Lower<T, U>( this Func<Option<T>, EnmOption<U>> func ) => i => func(i.ToOption()).Lower();

        
        [NotNull]
        public static Func<IEnumerable<T>, IEnumerable<U>> Lower<T, U>( this Func<EnmOption<T>, EnmOption<U>> func )
            => i => func(i.ToEnmOption()).Lower();

        /*
        public static Option<IEnumerable<T>> WhereNotEmpty<T> ([CanBeNull] this IEnumerable<Option<T>> enm) {
            if (enm == null)
                return Option<IEnumerable<T>>.None;

            return enm.Where(o => o.HasValue).Select(o => o.ValueOrFail()).ToOption();
        }*/
    }
}