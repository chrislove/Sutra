using System;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using static SharpPipe.Commands.func.takes<string>;

namespace SharpPipe {
    namespace CurryLib {

        /*
        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(Func<T1, T2, TResult> function) => a => b => function(a, b);


        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function) => a => b => c => function(a, b, c);

        public static Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> function)
            => a => b => c => d => function(a, b, c, d);
*/

        public static class str {
            public static Func<string, bool> Equals( string strB ) => str => str.Equals(strB);
            public static Func<string, bool> Equals( string value, StringComparison comparisonType ) => str => str.Equals(value, comparisonType);

            public static PipeFunc<string, int> Length => from( str => str.Length );

            public static ToSeqFunc<string, char> ToCharArray() => toseq( str => str.ToCharArray() );
            public static ToSeqFunc<string, char> ToCharArray( int startIndex, int length ) => toseq( str => str.ToCharArray(startIndex, length) );

            public static ToSeqFunc<string, string> Split( params char[] separator ) => toseq( str => str.Split(separator) );
            public static ToSeqFunc<string, string> Split( char[] separator, int count ) => toseq( str => str.Split(separator, count) );
            public static ToSeqFunc<string, string> Split( char[] separator, StringSplitOptions options ) => toseq( str => str.Split(separator, options) );
            public static ToSeqFunc<string, string> Split( char[] separator, int count, StringSplitOptions options )
                                => toseq(str => str.Split(separator, count, options));
            
            public static ToSeqFunc<string, string> Split( string[] separator, StringSplitOptions options ) => toseq(str => str.Split(separator, options));
            
            public static ToSeqFunc<string, string> Split( string[] separator, int count, StringSplitOptions options )
                                                    => toseq(str => str.Split(separator, count, options));
            
            public static Func<string, int> CompareTo( string strB ) => str => str.CompareTo(strB);
            
            
            
            public static Func<string, string> Substring( int startIndex ) => str => str.Substring(startIndex);
            public static Func<string, string> Substring( int startIndex, int length ) => str => str.Substring(startIndex, length);
            public static Func<string, string> Trim( params char[] trimChars ) => str => str.Trim(trimChars);
            public static Func<string, string> TrimStart( params char[] trimChars ) => str => str.TrimStart(trimChars);
            public static Func<string, string> TrimEnd( params char[] trimChars ) => str => str.TrimEnd(trimChars);
            public static Func<string, bool> IsNormalized() => str => str.IsNormalized();
            public static Func<string, bool> IsNormalized( NormalizationForm normalizationForm ) => str => str.IsNormalized(normalizationForm);
            public static Func<string, string> Normalize() => str => str.Normalize();
            public static Func<string, string> Normalize( NormalizationForm normalizationForm ) => str => str.Normalize(normalizationForm);
            public static Func<string, bool> Contains( string value ) => str => str.Contains(value);
            public static Func<string, bool> EndsWith( string value ) => str => str.EndsWith(value);
            public static Func<string, bool> EndsWith( string value, StringComparison comparisonType ) => str => str.EndsWith(value, comparisonType);
            public static Func<string, bool> EndsWith( string value, bool ignoreCase, CultureInfo culture ) => str => str.EndsWith(value, ignoreCase, culture);
            public static Func<string, int> IndexOf( char value ) => str => str.IndexOf(value);
            public static Func<string, int> IndexOf( char value, int startIndex ) => str => str.IndexOf(value, startIndex);
            public static Func<string, int> IndexOf( char value, int startIndex, int count ) => str => str.IndexOf(value, startIndex, count);
            public static Func<string, int> IndexOfAny( char[] anyOf ) => str => str.IndexOfAny(anyOf);
            public static Func<string, int> IndexOfAny( char[] anyOf, int startIndex ) => str => str.IndexOfAny(anyOf, startIndex);
            public static Func<string, int> IndexOfAny( char[] anyOf, int startIndex, int count ) => str => str.IndexOfAny(anyOf, startIndex, count);
            public static Func<string, int> IndexOf( string value ) => str => str.IndexOf(value);
            public static Func<string, int> IndexOf( string value, int startIndex ) => str => str.IndexOf(value, startIndex);
            public static Func<string, int> IndexOf( string value, int startIndex, int count ) => str => str.IndexOf(value, startIndex, count);
            public static Func<string, int> IndexOf( string value, StringComparison comparisonType ) => str => str.IndexOf(value, comparisonType);
            public static Func<string, int> IndexOf( string value, int startIndex, StringComparison comparisonType ) => str => str.IndexOf(value, startIndex, comparisonType);
            public static Func<string, int> IndexOf( string value, int startIndex, int count, StringComparison comparisonType ) => str => str.IndexOf(value, startIndex, count, comparisonType);
            public static Func<string, int> LastIndexOf( char value ) => str => str.LastIndexOf(value);
            public static Func<string, int> LastIndexOf( char value, int startIndex ) => str => str.LastIndexOf(value, startIndex);
            public static Func<string, int> LastIndexOf( char value, int startIndex, int count ) => str => str.LastIndexOf(value, startIndex, count);
            public static Func<string, int> LastIndexOfAny( char[] anyOf ) => str => str.LastIndexOfAny(anyOf);
            public static Func<string, int> LastIndexOfAny( char[] anyOf, int startIndex ) => str => str.LastIndexOfAny(anyOf, startIndex);
            public static Func<string, int> LastIndexOfAny( char[] anyOf, int startIndex, int count ) => str => str.LastIndexOfAny(anyOf, startIndex, count);
            public static Func<string, int> LastIndexOf( string value ) => str => str.LastIndexOf(value);
            public static Func<string, int> LastIndexOf( string value, int startIndex ) => str => str.LastIndexOf(value, startIndex);
            public static Func<string, int> LastIndexOf( string value, int startIndex, int count ) => str => str.LastIndexOf(value);
            public static Func<string, int> LastIndexOf( string value, StringComparison comparisonType ) => str => str.LastIndexOf(value, comparisonType);
            public static Func<string, int> LastIndexOf( string value, int startIndex, StringComparison comparisonType ) => str => str.LastIndexOf(value, startIndex, comparisonType);
            public static Func<string, int> LastIndexOf( string value, int startIndex, int count, StringComparison comparisonType ) => str => str.LastIndexOf(value, startIndex, count, comparisonType);
            public static Func<string, string> PadLeft( int totalWidth ) => str => str.PadLeft(totalWidth);
            public static Func<string, string> PadLeft( int totalWidth, char paddingChar ) => str => str.PadLeft(totalWidth, paddingChar);
            public static Func<string, string> PadRight( int totalWidth ) => str => str.PadRight(totalWidth);
            public static Func<string, string> PadRight( int totalWidth, char paddingChar ) => str => str.PadRight(totalWidth, paddingChar);
            public static Func<string, bool> StartsWith( string value ) => str => str.StartsWith(value);
            public static Func<string, bool> StartsWith( string value, StringComparison comparisonType ) => str => str.StartsWith(value, comparisonType);
            public static Func<string, bool> StartsWith( string value, bool ignoreCase, CultureInfo culture ) => str => str.StartsWith(value, ignoreCase, culture);
            public static Func<string, string> ToLower() => str => str.ToLower();
            public static Func<string, string> ToLower( CultureInfo culture ) => str => str.ToLower(culture);
            public static Func<string, string> ToLowerInvariant => str => str.ToLowerInvariant();
            public static Func<string, string> ToUpper() => str => str.ToUpper();
            public static Func<string, string> ToUpper( CultureInfo culture ) => str => str.ToUpper(culture);
            public static Func<string, string> ToUpperInvariant => str => str.ToUpperInvariant();
            public static Func<string, string> Trim() => str => str.Trim();
            public static Func<string, string> Insert( int startIndex, string value ) => str => str.Insert(startIndex, value);
            public static Func<string, string> Replace( char oldChar, char newChar ) => str => str.Replace(oldChar, newChar);
            public static Func<string, string> Replace( string oldValue, string newValue ) => str => str.Replace(oldValue, newValue);
            public static Func<string, string> Remove( int startIndex, int count ) => str => str.Remove(startIndex, count);
            public static Func<string, string> Remove( int startIndex ) => str => str.Remove(startIndex);

            public static Func<string, bool> IsNullOrEmpty => string.IsNullOrEmpty;
            public static Func<string, bool> IsNullOrWhiteSpace => string.IsNullOrWhiteSpace;

            public static FromSeqFunc<string, string> join( [NotNull] string separator ) => FromSeq(e => string.Join(separator, e));
            public static FromSeqFunc<string, string> concat => FromSeq(string.Concat);
        }
    }
}