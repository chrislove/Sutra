using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;


namespace SharpPipe
{
	[PublicAPI]
	public static partial class Commands {
		/// <summary>
		/// Returns pipe or sequence contents.
		/// </summary>
		/// <example><code>
		/// pipe | get
		/// </code></example>
		public static DoGet get => new DoGet();
	}

	/// <summary>
	/// Command marker.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DoGet {
		/// <summary>
		/// Makes the pipe return the actual value instead of an Option{T}.
		/// </summary>
		public static DoGetValue operator !( DoGet _ ) => new DoGetValue();
	}

	public struct DoGetValue { }

	partial struct Seq<T> {
		/// <summary>
		/// Returns pipe contents.
		/// </summary>
		[NotNull]
		public static IEnumerable<T> operator |( Seq<T> seq, DoGet _ ) => seq.Get.Contents;
	}

	public partial struct Pipe<T> {
		/// <summary>
		/// Returns pipe contents.
		/// </summary>
		[NotNull]
		public static T operator |( Pipe<T> pipe, DoGetValue _ ) => pipe.Get.ValueOrDefault;
	}
}