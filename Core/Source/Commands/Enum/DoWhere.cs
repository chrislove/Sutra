using System;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoWhere WHERE<T>(Func<T, bool> predicate) => WHERE(i => predicate(i.To<T>()));
        public static DoWhere WHERE(Func<dynamic, bool> predicate) => new DoWhere(predicate);
    }
    
    public struct DoWhere {
        internal readonly Func<object, bool> Predicate;

        internal DoWhere( [NotNull] Func<object, bool> predicate ) => Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

        /// <summary>
        /// A condition when the filter will be applied
        /// </summary>
        public static DoWhereIf operator *( DoWhere @where, DoIf doIf ) => new DoWhereIf(@where, doIf);

    }
    
    public struct DoWhereIf {
        private readonly DoWhere _where;
        private readonly DoIf _doIf;

        public DoWhereIf( DoWhere @where, DoIf doIf ) {
            _where = @where;
            _doIf   = doIf;
        }

        [NotNull] internal Func<object, bool> Predicate {
            get {
                var @this = this;
                return i => {
                           if (@this._doIf.Predicate(i))
                               return @this._where.Predicate(i);

                           return true;
                       };
            }
        }
    }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator -( EnumPipe<TOut> lhs, DoWhere @where ) {
            var filtered = lhs.Get.Where(i => @where.Predicate(i));

            return ENUM.IN(filtered);
        }
        
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator -( EnumPipe<TOut> lhs, DoWhereIf @where ) {
            var filtered = lhs.Get.Where(i => @where.Predicate(i));

            return ENUM.IN(filtered);
        }
    }
}