using System;
using System.Collections.Generic;

namespace SharpPipe {
    internal interface IOptionValue<out T> {
        T Value { get; }
    }
    
    public interface IOption<T> : IEnumerable<T>, IEquatable<IOption<T>> {
        bool HasValue { get; }
    }
}