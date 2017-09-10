using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe
{
	public static partial class Commands {
		/// <summary>
		/// Returns pipe contents.
		/// </summary>
		/// <example><code>
		/// pipe | OUT
		/// </code></example>
		public static DoOut ret => new DoOut();
	}
	
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DoOut {}
	
	partial struct Seq<T> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static IEnumerable<T> operator |( Seq<T> pipe, DoOut act ) => pipe.get;
	}

	public partial struct Pipe<T> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static T operator |( Pipe<T> pipe, DoOut doOut ) => pipe.get;
	}
}