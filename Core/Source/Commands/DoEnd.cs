﻿using System.Collections.Generic;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe
{
	public static partial class Commands {
		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		public static DoEnd OUT => new DoEnd();
	}
	
	public struct DoEnd {}
	
	partial struct EnumPipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static IEnumerable<TOut> operator |(EnumPipe<TOut> lhs, DoEnd act) => lhs.Get;
	}

	public partial struct Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator. Returns pipe contents.
		/// </summary>
		[NotNull]
		public static TOut operator |( Pipe<TOut> lhs, DoEnd doEnd ) => lhs.Get;
	}
}