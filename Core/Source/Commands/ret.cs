using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;


namespace SharpPipe
{
	[PublicAPI]
	public static partial class Commands {
		/// <summary>
		/// Returns pipe contents.
		/// </summary>
		/// <example><code>
		/// pipe | OUT
		/// </code></example>
		public static DoRet ret => new DoRet();
	}
	
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DoRet {}
	
	partial struct Seq<T> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static IEnumerable<T> operator |( Seq<T> pipe, DoRet act ) => pipe.Get;
	}

	public partial struct Pipe<T> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static T operator |( Pipe<T> pipe, DoRet doRet ) => pipe.Get;
	}
}