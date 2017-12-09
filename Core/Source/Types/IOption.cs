using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Sutra
{
    internal interface IOptionValue
    {
        object BoxedValue { get; }
    }

    internal interface IOptionValue<out T> : IOptionValue
    {
        T Value { get; }
    }

    public interface IOption
    {
        bool HasValue { get; }
    }

    public interface IOption<T> : IOption, IEquatable<IOption<T>> { }

    public interface ISimpleOption : IOption
    {
        [NotNull] [ItemNotNull] IEnumerable<object> Enm { get; }
    }

    public interface ISeqOption : IOption
    {
        [NotNull] [ItemNotNull] IEnumerable<IEnumerable<IOption>> Enm { get; }
    }

    public interface ISimpleOption<T> : IOption<T>, ISimpleOption
    {
        [NotNull] [ItemNotNull] new IEnumerable<T> Enm { get; }
    }

    public interface ISeqOption<T> : IOption<IEnumerable<Option<T>>>, ISeqOption
    {
        [NotNull] [ItemNotNull] new IEnumerable<IEnumerable<Option<T>>> Enm { get; }
    }
}