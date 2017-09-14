using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands
    {
        /// <summary>
        /// Evaluates to true if any value within the sequence matches the predicate.
        /// </summary>
        [NotNull]
        public static Func<ISeqOption, bool> any( Func<IOption, bool> predicate )
            => seq =>
                   {
                       foreach (IEnumerable<IOption> enm in seq)
                           return enm.Any(predicate);

                       return false; //Empty
                   };

        /*
        /// <summary>
        /// Evaluates to true if any value within the sequence matches the predicate.
        /// </summary>
        [NotNull]
        public static Func<ISeqOption, bool> any<T>( Func<T, bool> predicate )
            => seq =>
                   {
                       foreach (IEnumerable<IOption> enm in seq)
                           return enm.SelectNotEmpty<T>().Any(predicate);

                       return false; //Empty
                   };*/
        
        /// <summary>
        /// Evaluates to true if any value within the sequence matches the predicate.
        /// </summary>
        [NotNull]
        public static Func<ISeqOption, bool> any<T>( PipeFunc<T, bool> predicate )
            => seq =>
                   {
                       foreach (IEnumerable<IOption> enm in seq)
                           return enm.Cast<Option<T>>().Any<Option<T>>( predicate.LowerOut(false) );

                       return false; //Empty
                   };
    }
}