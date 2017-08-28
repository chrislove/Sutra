using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class Pipe {
		[NotNull] public static GetPipe PIPE => GetPipe.Empty;

		[NotNull] public static PipeEnd<T> END<T>() => new PipeEnd<T>();

		[NotNull] public static PipeEnd<dynamic> DYN => new PipeEnd<dynamic>();
		[NotNull] public static PipeEnd<object> OBJ => new PipeEnd<object>();

		[NotNull] public static PipeEnd<string> STR => END<string>();
		[NotNull] public static PipeEnd<short> SHORT => END<short>();
		[NotNull] public static PipeEnd<ushort> USHORT => END<ushort>();
		[NotNull] public static PipeEnd<int> INT => END<int>();
		[NotNull] public static PipeEnd<uint> UINT => END<uint>();
		[NotNull] public static PipeEnd<long> LONG => END<long>();
		[NotNull] public static PipeEnd<ulong> ULONG => END<ulong>();
		[NotNull] public static PipeEnd<bool> BOOL => END<bool>();
		[NotNull] public static PipeEnd<float> FLT => END<float>();
		[NotNull] public static PipeEnd<double> DBL => END<double>();
		[NotNull] public static PipeEnd<decimal> DEC => END<decimal>();
		[NotNull] public static PipeEnd<DateTime> DTTM => END<DateTime>();
		[NotNull] public static PipeEnd<Array> ARR => END<Array>();
		[NotNull] public static PipeEnd<byte> BYTE => END<byte>();
		[NotNull] public static PipeEnd<char> CHAR => END<char>();

		/// <summary>
		/// Evaluates a pipe.
		/// </summary>
		/// <example>
		/// <code>
		///    _( PIPE | DateTime.Now | Print );
		/// </code>
		/// </example>
		public static void _(ActPipe pipe) => pipe.Do();

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime}( p => GetDate(p) )
		/// </code>
		/// </example>
		/// <typeparam name="TParam">Function parameter type.</typeparam>
		/// <param name="func">Function to convert</param>
		[NotNull] public static F _<TParam>(Func<TParam, object> func) => p => func(p.To<TParam>());

		/// <summary>
		/// Creates a pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _( p => DoStuff(p) )
		/// </code>
		/// </example>
		/// <param name="func">Function to convert</param>
		[NotNull] public static F _(Func<dynamic, object> func) => p => func(p);

		/// <summary>
		/// Creates a strongly-typed pipe-compatible action.
		/// </summary>
		/// <example>
		/// <code>
		///    __{string}(Console.WriteLine)
		/// </code>
		/// </example>
		/// <typeparam name="TParam">Action parameter type.</typeparam>
		/// <param name="act">Action to convert</param>
		[NotNull] public static A __<TParam>(Action<TParam> act) => p => act(p.To<TParam>());

		/// <summary>
		/// Creates a pipe-compatible action.
		/// </summary>
		/// <example>
		/// <code>
		///    __(p => Console.WriteLine()p)
		/// </code>
		/// </example>
		/// <param name="act">Action to convert</param>
		[NotNull] public static A __(Action<dynamic> act) => p => act(p);

	}
}