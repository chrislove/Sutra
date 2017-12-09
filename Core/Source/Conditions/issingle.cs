using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra {
    internal static partial class Conditions
    {
        /// <summary>
        /// Evaluates to true if the sequence contains a single element.
        /// </summary>
        [PublicAPI] [NotNull] public static Func<ISeqOption, bool> issingle
            => seq =>
                   {
                       foreach (IEnumerable<IOption> enm in seq.Enm)
                           return enm.Count() == 1;

                       return false; //Empty
                   };
    
        /// <summary>
        /// Evaluates to true if the sequence contains zero or more than one elements.
        /// </summary>
        [PublicAPI] [NotNull]
        public static Func<ISeqOption, bool> notsingle => seq => !issingle(seq);
    }
}