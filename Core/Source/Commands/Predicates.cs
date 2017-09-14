using System;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe
{
    public static partial class Commands
    {
        public static Unit unit => new Unit();

        [NotNull] public static Func<IOption, bool> notempty => i => !isempty(i);

        [NotNull] public static Func<ISeqOption, bool> issingle
            => seq =>
                   {
                       foreach (var enm in seq)
                           return enm.Count() == 1;

                       return false;    //Empty
                   };
        
        [NotNull] public static Func<ISeqOption, bool> notsingle => seq => !issingle(seq);

        public static Func<T, bool> not<T>( Func<T, bool> func ) => i => !func(i);

        public static PipeFunc<T, bool> not<T>( PipeFunc<T, bool> func )
            => new PipeFunc<T, bool>( opt => opt.HasValue ? func[opt].Map(i => !i) : Option<bool>.None );

        [NotNull]
        public static Func<IOption, bool> equals<T>( Option<T> obj ) => i => obj == i;

        [NotNull]
        public static Func<IOption, bool> notequals<T>( Option<T> obj ) => i => !equals(obj)(i);

        [NotNull]
        public static Func<IOption, bool> equals<T>( T obj ) => i => (Option<T>) i == obj;

        [NotNull]
        public static Func<IOption, bool> notequals<T>( T obj ) => i => !equals(obj)(i);
    }
}