using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe
{
    public static partial class Commands
    {
        /// <summary>
        /// Returns true if the pipe or sequence is empty.
        /// </summary>
        /// <exception cref="EmptyPipeException"></exception>
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
                                   throw new SharpPipeException($"isempty not implemented for {i.GetType()}");
                           }
                   };
    }
}