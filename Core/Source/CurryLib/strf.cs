using System;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    namespace CurryLib {
        [PublicAPI]
        public static class strf {
            public static Func<string, bool> equals( string strB ) => str => str.Equals(strB);
            public static Func<string, bool> equals( string value, StringComparison comparisonType ) => str => str.Equals(value, comparisonType);

            public static PipeFunc<string, int> length => fun( str => str.Length );
            public static Func<string, int> compareTo( string strB ) => str => str.CompareTo(strB);
            
            public static Func<string, string> substring( int startIndex ) => str => str.Substring(startIndex);
            public static Func<string, string> substring( int startIndex, int length ) => str => str.Substring(startIndex, length);
            public static Func<string, string> trim( params char[] trimChars ) => str => str.Trim(trimChars);
            public static Func<string, string> trimStart( params char[] trimChars ) => str => str.TrimStart(trimChars);
            public static Func<string, string> trimEnd( params char[] trimChars ) => str => str.TrimEnd(trimChars);
            public static Func<string, bool> isNormalized() => str => str.IsNormalized();
            public static Func<string, bool> isNormalized( NormalizationForm normalizationForm ) => str => str.IsNormalized(normalizationForm);
            public static Func<string, string> normalize() => str => str.Normalize();
            public static Func<string, string> normalize( NormalizationForm normalizationForm ) => str => str.Normalize(normalizationForm);
            public static Func<string, bool> contains( string value ) => str => str.Contains(value);
            public static Func<string, bool> endsWith( string value ) => str => str.EndsWith(value);
            public static Func<string, bool> endsWith( string value, StringComparison comparisonType ) => str => str.EndsWith(value, comparisonType);
            public static Func<string, bool> endsWith( string value, bool ignoreCase, CultureInfo culture ) => str => str.EndsWith(value, ignoreCase, culture);
            public static Func<string, int> indexOf( char value ) => str => str.IndexOf(value);
            public static Func<string, int> indexOf( char value, int startIndex ) => str => str.IndexOf(value, startIndex);
            public static Func<string, int> indexOf( char value, int startIndex, int count ) => str => str.IndexOf(value, startIndex, count);
            public static Func<string, int> indexOfAny( char[] anyOf ) => str => str.IndexOfAny(anyOf);
            public static Func<string, int> indexOfAny( char[] anyOf, int startIndex ) => str => str.IndexOfAny(anyOf, startIndex);
            public static Func<string, int> indexOfAny( char[] anyOf, int startIndex, int count ) => str => str.IndexOfAny(anyOf, startIndex, count);
            public static Func<string, int> indexOf( string value ) => str => str.IndexOf(value);
            public static Func<string, int> indexOf( string value, int startIndex ) => str => str.IndexOf(value, startIndex);
            public static Func<string, int> indexOf( string value, int startIndex, int count ) => str => str.IndexOf(value, startIndex, count);
            public static Func<string, int> indexOf( string value, StringComparison comparisonType ) => str => str.IndexOf(value, comparisonType);
            public static Func<string, int> indexOf( string value, int startIndex, StringComparison comparisonType ) => str => str.IndexOf(value, startIndex, comparisonType);
            public static Func<string, int> indexOf( string value, int startIndex, int count, StringComparison comparisonType ) => str => str.IndexOf(value, startIndex, count, comparisonType);
            public static Func<string, int> lastIndexOf( char value ) => str => str.LastIndexOf(value);
            public static Func<string, int> lastIndexOf( char value, int startIndex ) => str => str.LastIndexOf(value, startIndex);
            public static Func<string, int> lastIndexOf( char value, int startIndex, int count ) => str => str.LastIndexOf(value, startIndex, count);
            public static Func<string, int> lastIndexOfAny( char[] anyOf ) => str => str.LastIndexOfAny(anyOf);
            public static Func<string, int> lastIndexOfAny( char[] anyOf, int startIndex ) => str => str.LastIndexOfAny(anyOf, startIndex);
            public static Func<string, int> lastIndexOfAny( char[] anyOf, int startIndex, int count ) => str => str.LastIndexOfAny(anyOf, startIndex, count);
            public static Func<string, int> lastIndexOf( string value ) => str => str.LastIndexOf(value);
            public static Func<string, int> lastIndexOf( string value, int startIndex ) => str => str.LastIndexOf(value, startIndex);
            public static Func<string, int> lastIndexOf( string value, int startIndex, int count ) => str => str.LastIndexOf(value);
            public static Func<string, int> lastIndexOf( string value, StringComparison comparisonType ) => str => str.LastIndexOf(value, comparisonType);
            public static Func<string, int> lastIndexOf( string value, int startIndex, StringComparison comparisonType ) => str => str.LastIndexOf(value, startIndex, comparisonType);
            public static Func<string, int> lastIndexOf( string value, int startIndex, int count, StringComparison comparisonType ) => str => str.LastIndexOf(value, startIndex, count, comparisonType);
            public static Func<string, string> padLeft( int totalWidth ) => str => str.PadLeft(totalWidth);
            public static Func<string, string> padLeft( int totalWidth, char paddingChar ) => str => str.PadLeft(totalWidth, paddingChar);
            public static Func<string, string> padRight( int totalWidth ) => str => str.PadRight(totalWidth);
            public static Func<string, string> padRight( int totalWidth, char paddingChar ) => str => str.PadRight(totalWidth, paddingChar);
            public static Func<string, bool> startsWith( string value ) => str => str.StartsWith(value);
            public static Func<string, bool> startsWith( string value, StringComparison comparisonType ) => str => str.StartsWith(value, comparisonType);
            public static Func<string, bool> startsWith( string value, bool ignoreCase, CultureInfo culture ) => str => str.StartsWith(value, ignoreCase, culture);
            public static Func<string, string> toLower() => str => str.ToLower();
            public static Func<string, string> toLower( CultureInfo culture ) => str => str.ToLower(culture);
            public static Func<string, string> toLowerInvariant => str => str.ToLowerInvariant();
            public static Func<string, string> toUpper() => str => str.ToUpper();
            public static Func<string, string> toUpper( CultureInfo culture ) => str => str.ToUpper(culture);
            public static Func<string, string> toUpperInvariant => str => str.ToUpperInvariant();
            public static Func<string, string> trim() => str => str.Trim();
            public static Func<string, string> Insert( int startIndex, string value ) => str => str.Insert(startIndex, value);
            public static Func<string, string> replace( char oldChar, char newChar ) => str => str.Replace(oldChar, newChar);
            public static Func<string, string> replace( string oldValue, string newValue ) => str => str.Replace(oldValue, newValue);
            public static Func<string, string> remove( int startIndex, int count ) => str => str.Remove(startIndex, count);
            public static Func<string, string> remove( int startIndex ) => str => str.Remove(startIndex);

            public static Func<string, bool> isNullOrEmpty => string.IsNullOrEmpty;
            public static Func<string, bool> isNullOrWhiteSpace => string.IsNullOrWhiteSpace;

            public static ToSeqFunc<string, char> toCharArray() => toseq(str => str.ToCharArray() );
            public static ToSeqFunc<string, char> toCharArray( int startIndex, int length ) => toseq( str => str.ToCharArray(startIndex, length) );

            public static ToSeqFunc<string, string> split( params char[] separator ) => toseq( str => str.Split(separator) );
            public static ToSeqFunc<string, string> split( char[] separator, int count ) => toseq( str => str.Split(separator, count) );
            public static ToSeqFunc<string, string> split( char[] separator, StringSplitOptions options ) => toseq( str => str.Split(separator, options) );
            public static ToSeqFunc<string, string> split( char[] separator, int count, StringSplitOptions options )
                => toseq(str => str.Split(separator, count, options));
            
            public static ToSeqFunc<string, string> split( string[] separator, StringSplitOptions options ) => toseq(str => str.Split(separator, options));
            
            public static ToSeqFunc<string, string> split( string[] separator, int count, StringSplitOptions options )
                => toseq(str => str.Split(separator, count, options));
            
            public static FromSeqFunc<string, string> join( [NotNull] string separator ) => fromseq<string>.fun(e => string.Join(separator, e));
            public static FromSeqFunc<string, string> concat => fromseq<string>.fun(string.Concat);
        }
    }
}