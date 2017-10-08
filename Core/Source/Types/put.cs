using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands
    {
        /// <summary>
        /// Replaces contents of the pipe with a new value of a different type.
        /// </summary>
        public static DoPut<T> put<T>(T value) => new DoPut<T>(value);
     }
 
     public struct DoPut<T> {
         private readonly T _value;
 
         public DoPut( T value ) => _value = value;
 
         public static Pipe<T> operator |( IPipe pipe, DoPut<T> doPut ) => start<T>.pipe | doPut._value;
     }
 }