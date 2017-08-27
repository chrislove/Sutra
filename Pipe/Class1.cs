using NUnit.Framework;
using System;
using static Tests.PipeBase;

//using static Tests.PipeBase;

namespace Tests
{
	public static class DelegateUtility
	{
		public static T Cast<T>(Delegate source) where T : class
		{
			return Cast(source, typeof(T)) as T;
		}

		public static Delegate Cast(Delegate source, Type type)
		{
			if (source == null)
				return null;
			Delegate[] delegates = source.GetInvocationList();
			if (delegates.Length == 1)
				return Delegate.CreateDelegate(type,
					delegates[0].Target, delegates[0].Method);
			Delegate[] delegatesDest = new Delegate[delegates.Length];
			for (int nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
				delegatesDest[nDelegate] = Delegate.CreateDelegate(type,
					delegates[nDelegate].Target, delegates[nDelegate].Method);
			return Delegate.Combine(delegatesDest);
		}
	}

	public static class TypeExtensions {
		/// <summary>
		/// Compares object with a given type.
		/// </summary>
		public static bool Is<T>( this Type type ) {
			return type == typeof(T);
		}

		/// <summary>
		/// Casts object to a given type.
		/// </summary>
		public static T To<T>( this object obj ) {
			try {
				return (T) obj;
			} catch (Exception) {
				throw new InvalidOperationException($"Unable to cast type [{obj.GetType()}] to {typeof(T)}.");
			}
		}
	}
	public static class FuncBaseExtensions {
		/*public static PipeBase F( this F func ) => new PipeBase(func);
		public static PipeBase A( this A act ) => new PipeBase(act);
		public static PipeBase F( this object obj ) => new PipeBase(i => obj);*/

		//public static F F<TIn, TOut>(Func<TIn, TOut> func ) => i => func(i);

		//public static F ToF(Delegate func) => date => func(date());
		//public static F DynFunc(this Func<dynamic, dynamic> func) => date => func(date);

		//public static Func<dynamic, dynamic> ToDynamicFunc() => 

	}

	[TestFixture]
	public class Tests {
		private F Add( int b ) => a => (int) a + b;
		private F Mult( int b ) => a => (int) a * b;
		private F Mult( float b ) => a => (float) a * b;

		private F AddDays( int days ) => date => date.To<DateTime>().AddDays(days);

		private string GetLongDate( DateTime date ) => date.ToLongDateString();

		private F GetLongDateF() => i => GetLongDate(i.To<DateTime>());

		private F GetShortDate() => date => date.To<DateTime>().ToShortDateString();

		private A WriteToConsole() => Console.WriteLine;

		[Test]
		public void Test() {
			Pipe(P | DateTime.Now | AddDays(-1) | GetShortDate() | (A)Console.WriteLine);

			var yesterday = Pipe( P | DateTime.Now | AddDays(-1) | GetShortDate() | (A)Console.WriteLine) );
			yesterday.Do();
			//Console.WriteLine(yesterday.Get());

			var seq = P | DateTime.Now | AddDays(10) | GetLongDateF() | WriteToConsole();
			seq.Do();
		}
	}
}