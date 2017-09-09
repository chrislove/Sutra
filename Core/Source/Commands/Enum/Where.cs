using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        //public static DoWhere WHERE<T>(Func<T, bool> predicate) => WHERE(i => predicate(i.To<T>()));
        public static DoWhere WHERE => new DoWhere();
    }

    public struct DoWhere { }

    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static DoWhere<TOut> operator |( EnumPipe<TOut> pipe, DoWhere @where ) => new DoWhere<TOut>(pipe);
    }
    

    public struct DoWhere<T> {
        internal readonly EnumPipe<T> Pipe;

        internal DoWhere( EnumPipe<T> pipe ) => Pipe = pipe;

        public static EnumPipe<T> operator |( DoWhere<T> @where, Func<T, bool> predicate ) => where.Pipe.Get.Where(predicate) | TO<T>.PIPE;
        
        public static DoWhereIf<T> operator |( DoWhere<T> @where, DoIf doIf ) => new DoWhereIf<T>(@where);
    }

    public struct DoWhereIf<T> {
        private readonly DoWhere<T> _doWhere;

        internal DoWhereIf( DoWhere<T> doWhere ) => _doWhere = doWhere;

        public static EnumPipe<T> operator |( DoWhereIf<T> doWhereIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = doWhereIf._doWhere.Pipe;
            var filtered = pipe.Get.Where(predicate);

            return NEW<T>.PIPE | filtered;
        }
    }
    
    /*
    public struct DoWhereIf {
        private readonly DoWhere _doWhere;
        private readonly DoIf _doIf;

        public DoWhereIf( DoWhere<T> doWhere, DoIf doIf ) {
            _doWhere = doWhere;
            _doIf   = doIf;
        }

        [NotNull] internal Func<object, bool> Predicate {
            get {
                var @this = this;
                return i => {
                           if (@this._doIf.Predicate(i))
                               return @this._doWhere.Predicate(i);

                           return true;
                       };
            }
        }
    }*/
    
    /*
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoWhere<TOut> @where ) {
            var filtered = lhs.Get.Where(i => @where.Predicate(i));

            return ENUM.IN(filtered);
        }
        
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoWhereIf @where ) {
            var filtered = lhs.Get.Where(i => @where.Predicate(i));

            return ENUM.IN(filtered);
        }
    }*/
}