namespace SharpPipe {
    /*
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if the underlying sequence collection is empty
        /// </summary>
        public static DoNotEmpty notempty => new DoNotEmpty();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoNotEmpty {}

    public partial struct Seq<T> {
        public static Seq<T> operator |( Seq<T> seq, DoNotEmpty _ ) {
            var exception = EmptyPipeException.For<Seq<T>>();
            return seq | fail | exception | when | (Func<IEnumerable<T>, bool>) isempty;
        }
    }*/
}