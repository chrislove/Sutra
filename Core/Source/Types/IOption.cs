using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpPipe
{
    internal interface IOptionValue
    {
        object BoxedValue { get; }
    }

    internal interface IOptionValue<out T> : IOptionValue
    {
        T Value { get; }
    }

    public interface IOption : IEnumerable
    {
        bool HasValue { get; }
    }

    public interface IOption<T> : IOption, IEnumerable<T>, IEquatable<IOption<T>> { }

    public interface ISimpleOption : IOption { }

    public interface ISeqOption : IOption, IEnumerable<IEnumerable<IOption>>
    {
        Option<IEnumerable<object>> Lower();
    }

    public interface ISimpleOption<T> : IOption<T>, ISimpleOption { }

    public interface ISeqOption<T> : IOption<IEnumerable<Option<T>>>, ISeqOption
    {
        Option<IEnumerable<T>> Lower();
    }
}