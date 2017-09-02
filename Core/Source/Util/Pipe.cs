﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe
{
	public static partial class Pipe
	{
		internal static Pipe<T> FromObject<T>(T obj) => FromFunc(SharpFunc.WithValue(obj));
		internal static Pipe<T> FromFunc<T>(IOutFunc<T> func) => new Pipe<T>(func);

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		public static Pipe<TOut> IN<TOut>([NotNull] TOut obj) => Pipe.FromObject(obj);

		/// <summary>
		/// Initializes EnumPipe with contents.
		/// </summary>
		public static EnumPipe<TOut> ENUM<TOut>( [NotNull] params TOut[] objs ) => EnumPipe.FromEnumerable(objs);

		/// <summary>
		/// Initializes EnumPipe with IEnumerable{T}
		/// </summary>
		public static EnumPipe<TOut> ENUM<TOut>([NotNull] IEnumerable<TOut> obj) => EnumPipe.FromEnumerable(obj);

		/// <summary>
		/// Creates an empty EnumPipe{T}
		/// </summary>
		public static EnumPipe<TOut> ENUM<TOut>() => EnumPipe.FromEnumerable( Enumerable.Empty<TOut>() );
		
		/// <summary>
		/// Executes a SharpAct.
		/// </summary>
		/// <example>
		/// <code>
		///    _( PIPE | DateTime.Now | Print );
		/// </code>
		/// </example>
		public static void _( [NotNull] SharpAct act ) => act.Action(null);

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime}( p => GetDate(p) )
		/// </code>
		/// </example>
		[NotNull] public static SharpFunc<TOut> _<TOut>([CanBeNull] Func<object, TOut> func) => SharpFunc.FromFunc(func);


		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime, string}( p => GetDate(p) )
		/// </code>
		/// </example>
		[NotNull] public static SharpFunc<TIn, TOut> _<TIn, TOut>([CanBeNull] Func<TIn, TOut> func) => SharpFunc.FromFunc(func);
		//[NotNull] public static EnumFunc<TIn, TOut> __<TIn, TOut>([CanBeNull] Func<TIn, IEnumerable<TOut>> func) => EnumFunc.FromFunc(func);

		[NotNull] public static SharpAct<TIn> _<TIn>([CanBeNull] Action<TIn> act) => SharpAct.FromAction<TIn>(i => act(i.To<TIn>()));
	}
}