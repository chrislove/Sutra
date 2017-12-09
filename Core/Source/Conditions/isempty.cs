using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra {
    internal static partial class Conditions
    {
        /// <summary>
        /// Evaluates to true if the pipe or sequence is empty.
        /// </summary>
        [NotNull] public static Func<IOption, bool> isempty
            => i =>
                   {
                       NextException.SetFactory(() => ExceptionFactory.IsEmpty(i));

                       switch (i)
                           {
                               case ISimpleOption option:
                                   return !option.HasValue;

                               case ISeqOption seq:
                                   foreach (IEnumerable<IOption> enm in seq.Enm)
                                       return !enm.Any();

                                   return !seq.HasValue;

                               default:
                                   throw new SutraException($"isempty not implemented for {i.GetType()}");
                           }
                   };
        
        
        /// <summary>
        /// Evaluates to true if the pipe or sequence is not empty.
        /// </summary>
        [PublicAPI] [NotNull]
        public static Func<IOption, bool> notempty => i => !isempty(i);
   
    }
}