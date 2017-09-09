using System.Collections.Generic;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe
{
	public static partial class Commands {
		/// <summary>
		/// Returns pipe contents.
		/// </summary>
		public static DoOut OUT => new DoOut();
	}
	
	public struct DoOut {}
	
	partial struct EnumPipe<T> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static IEnumerable<T> operator |( EnumPipe<T> pipe, DoOut act ) => pipe.Get;
	}

	public partial struct Pipe<T> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static T operator |( Pipe<T> pipe, DoOut doOut ) => pipe.Get;
	}
}