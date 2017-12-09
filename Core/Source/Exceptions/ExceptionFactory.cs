using System;
using JetBrains.Annotations;

namespace Sutra {
    internal static class ExceptionFactory
    {
        [NotNull]
        public static Exception IsEmpty( object inObj )
            {
                Type objType = inObj.GetType();
                                
                if (typeof(ISimpleOption).IsAssignableFrom(objType))
                    return EmptyPipeException.For(objType.GetFirstGenericArg());
                
                if (typeof(ISeqOption).IsAssignableFrom(objType))
                    return EmptySequenceException.For(objType.GetFirstGenericArg());

                throw new SutraException($"ExceptionFactory.IsEmpty not implemented for {objType}");
            }
    }
}