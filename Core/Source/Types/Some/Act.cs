using System;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe
{
    public partial struct Act
    {
        public static Act<T> From<T>(Fun<T, Unit> fun) => new Act<T>(fun);
        public static Act<T> From<T>(Func<T, Unit> fun) => new Act<T>(fun);
        public static Act<T> From<T>(Action<T> act) => new Act<T>(act.ReturnsUnit());
        
        public static Act From(Fun<Unit> fun) => new Act(fun);
        public static Act From(Func<Unit> fun) => new Act(SharpPipe.Fun.From(fun));
        public static Act From(Action act) => Act.From(act.ReturnsUnit());
    }
    
    /// <summary>
    /// An Action that is guaranteed to be non-null.
    /// </summary>
    public struct Act<T>
    {
        [PublicAPI] public Fun<T, Unit> Fun { get; }

        [PublicAPI]
        public Unit Invoke( [CanBeNull] T arg ) => Fun[arg];
        
        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public Unit this[ [CanBeNull] T inValue ] => Fun[inValue];


        public Act( Fun<T, Unit> fun )
            {
                if (fun == null)
                    throw new InvalidInputException($"Trying to create Act<{typeof(T)}> from a null value.");

                Fun = fun;
            }

        public static implicit operator Func<T, Unit>( Act<T> act ) => act.Fun;
        public static implicit operator Action<T>( Act<T> act ) => act.Fun.ToAction();
        public static implicit operator Act<T>( Action<T> action ) => new Act<T>(action.ReturnsUnit());
    }

    /// <summary>
    /// An Action that is guaranteed to be non-null.
    /// </summary>
    public partial struct Act
    {
        [PublicAPI] public Fun<Unit> Fun { get; }

        [PublicAPI]
        public Unit Invoke() => Fun.Func();
        
        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public Unit this[ Unit _ ] => Fun | inv;

        public Act( Fun<Unit> fun )
            {
                if (fun == null)
                    throw new InvalidInputException($"Trying to create Act from a null value.");

                Fun = fun;
            }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public static Unit operator |( Act act, DoInvoke _ ) => act.Fun | inv;

        public static implicit operator Action( Act act ) => act.Fun.ToAction();
    }
}