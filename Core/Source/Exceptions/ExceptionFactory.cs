using System;
using JetBrains.Annotations;

namespace SharpPipe {
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

                throw new SharpPipeException($"ExceptionFactory.IsEmpty not implemented for {objType}");
            }
    }
}