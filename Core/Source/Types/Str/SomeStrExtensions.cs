using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static Sutra.Commands;

namespace Sutra {
    public static class SomeStrExtensions
    {
        public static bool Equals( this somestr str, somestr strB ) => str._.Equals(strB);
        public static bool Equals( this somestr str, somestr value, StringComparison comparisonType ) => str._.Equals(value, comparisonType);

        public static int Length(this somestr str) => str._.Length;
        public static int CompareTo(this somestr str,  somestr strB ) => str._.CompareTo(strB);
            
        public static somestr Substring(this somestr str,  int startIndex ) => str._.Substring(startIndex) | some;
        public static somestr Substring(this somestr str,   int startIndex, int length ) => str._.Substring(startIndex, length) | some;
        public static somestr Trim(this somestr str,   params char[] trimChars ) => str._.Trim(trimChars) | some;
        public static somestr TrimStart(this somestr str,   params char[] trimChars ) => str._.TrimStart(trimChars) | some;
        public static somestr TrimEnd(this somestr str,   params char[] trimChars ) => str._.TrimEnd(trimChars) | some;
        public static bool IsNormalized(this somestr str) => str._.IsNormalized();
        public static bool IsNormalized(this somestr str, NormalizationForm normalizationForm ) => str._.IsNormalized(normalizationForm);
        public static somestr Normalize(this somestr str) => str._.Normalize() | some;
        public static somestr Normalize(this somestr str, NormalizationForm normalizationForm ) => str._.Normalize(normalizationForm) | some;
        public static bool Contains(this somestr str, somestr value ) => str._.Contains(value);
        public static bool EndsWith(this somestr str, somestr value ) => str._.EndsWith(value);
        public static bool EndsWith(this somestr str, somestr value, StringComparison comparisonType ) => str._.EndsWith(value, comparisonType);
        public static bool EndsWith(this somestr str, somestr value, bool ignoreCase, CultureInfo culture ) => str._.EndsWith(value, ignoreCase, culture);
        public static int IndexOf(this somestr str, char value ) => str._.IndexOf(value);
        public static int IndexOf(this somestr str, char value, int startIndex ) => str._.IndexOf(value, startIndex);
        public static int IndexOf(this somestr str, char value, int startIndex, int count ) => str._.IndexOf(value, startIndex, count);
        public static int IndexOfAny(this somestr str, char[] anyOf ) => str._.IndexOfAny(anyOf);
        public static int IndexOfAny(this somestr str, char[] anyOf, int startIndex ) => str._.IndexOfAny(anyOf, startIndex);
        public static int IndexOfAny(this somestr str, char[] anyOf, int startIndex, int count ) => str._.IndexOfAny(anyOf, startIndex, count);
        public static int IndexOf(this somestr str, somestr value ) => str._.IndexOf(value);
        public static int IndexOf( this somestr str, somestr value, int startIndex ) => str._.IndexOf(value, startIndex);
        public static int IndexOf( this somestr str, somestr value, int startIndex, int count ) => str._.IndexOf(value, startIndex, count);
        public static int IndexOf( this somestr str, somestr value, StringComparison comparisonType ) => str._.IndexOf(value, comparisonType);
        public static int IndexOf( this somestr str, somestr value, int startIndex, StringComparison comparisonType ) => str._.IndexOf(value, startIndex, comparisonType);
        public static int IndexOf( this somestr str, somestr value, int startIndex, int count, StringComparison comparisonType ) => str._.IndexOf(value, startIndex, count, comparisonType);
        public static int LastIndexOf( this somestr str, char value ) => str._.LastIndexOf(value);
        public static int LastIndexOf( this somestr str, char value, int startIndex ) => str._.LastIndexOf(value, startIndex);
        public static int LastIndexOf( this somestr str, char value, int startIndex, int count ) => str._.LastIndexOf(value, startIndex, count);
        public static int LastIndexOfAny( this somestr str, char[] anyOf ) => str._.LastIndexOfAny(anyOf);
        public static int LastIndexOfAny( this somestr str, char[] anyOf, int startIndex ) => str._.LastIndexOfAny(anyOf, startIndex);
        public static int LastIndexOfAny( this somestr str, char[] anyOf, int startIndex, int count ) => str._.LastIndexOfAny(anyOf, startIndex, count);
        public static int LastIndexOf( this somestr str, somestr value ) => str._.LastIndexOf(value);
        public static int LastIndexOf( this somestr str, somestr value, int startIndex ) => str._.LastIndexOf(value, startIndex);
        public static int LastIndexOf( this somestr str, somestr value, int startIndex, int count ) => str._.LastIndexOf(value);
        public static int LastIndexOf( this somestr str, somestr value, StringComparison comparisonType ) => str._.LastIndexOf(value, comparisonType);
        public static int LastIndexOf( this somestr str, somestr value, int startIndex, StringComparison comparisonType ) => str._.LastIndexOf(value, startIndex, comparisonType);
        public static int LastIndexOf( this somestr str, somestr value, int startIndex, int count, StringComparison comparisonType ) => str._.LastIndexOf(value, startIndex, count, comparisonType);
        public static somestr PadLeft( this somestr str, int totalWidth ) => str._.PadLeft(totalWidth) | some;
        public static somestr PadLeft( this somestr str, int totalWidth, char paddingChar ) => str._.PadLeft(totalWidth, paddingChar) | some;
        public static somestr PadRight( this somestr str, int totalWidth ) => str._.PadRight(totalWidth) | some;
        public static somestr PadRight( this somestr str, int totalWidth, char paddingChar ) => str._.PadRight(totalWidth, paddingChar) | some;
        public static bool StartsWith( this somestr str, somestr value ) => str._.StartsWith(value);
        public static bool StartsWith( this somestr str, somestr value, StringComparison comparisonType ) => str._.StartsWith(value, comparisonType);
        public static bool StartsWith( this somestr str, somestr value, bool ignoreCase, CultureInfo culture ) => str._.StartsWith(value, ignoreCase, culture);
        public static somestr ToLower(this somestr str) => str._.ToLower() | some;
        public static somestr ToLower( this somestr str, CultureInfo culture ) => str._.ToLower(culture) | some;
        public static somestr ToLowerInvariant(this somestr str) => str._.ToLowerInvariant() | some;
        public static somestr ToUpper(this somestr str) => str._.ToUpper() | some;
        public static somestr ToUpper( this somestr str, CultureInfo culture ) => str._.ToUpper(culture) | some;
        public static somestr ToUpperInvariant(this somestr str) => str._.ToUpperInvariant() | some;
        public static somestr Trim(this somestr str) => str._.Trim() | some;
        public static somestr Insert( this somestr str, int startIndex, somestr value ) => str._.Insert(startIndex, value) | some;
        public static somestr Replace( this somestr str, char oldChar, char newChar ) => str._.Replace(oldChar, newChar) | some;
        public static somestr Replace( this somestr str, somestr oldValue, somestr newValue ) => str._.Replace(oldValue, newValue) | some;
        public static somestr Remove( this somestr str, int startIndex, int count ) => str._.Remove(startIndex, count) | some;
        public static somestr Remove( this somestr str, int startIndex ) => str._.Remove(startIndex) | some;

        public static bool IsEmpty(this somestr str) => string.IsNullOrEmpty(str);
        public static bool IsWhiteSpace(this somestr str) => string.IsNullOrWhiteSpace(str);

        public static char[] ToCharArray(this somestr str) => str._.ToCharArray();
        public static char[] ToCharArray(this somestr str, int startIndex, int length ) => str._.ToCharArray(startIndex, length);

        public static somestr[] Split( this somestr str, params char[] separator ) => str._.Split(separator).Select(i => i | some).ToArray();
        public static somestr[] Split( this somestr str, char[] separator, int count ) => str._.Split(separator, count).Select(i => i | some).ToArray();
        public static somestr[] Split( this somestr str, char[] separator, StringSplitOptions options ) => str._.Split(separator, options).Select(i => i | some).ToArray();
        public static somestr[] Split( this somestr str, char[] separator, int count, StringSplitOptions options ) => str._.Split(separator, count, options).Select(i => i | some).ToArray();
            
        public static somestr[] Split( this somestr str, somestr[] separator, StringSplitOptions options ) => str._.Split(separator.Select(i => i._).ToArray(), options).Select(i => i | some).ToArray();
            
        public static somestr[] Split( this somestr str, somestr[] separator, int count, StringSplitOptions options )
            => str._.Split(separator.Select(i => i._).ToArray(), count, options).Select(i => i | some).ToArray();
            
        public static somestr Join( this IEnumerable<somestr> enm, somestr separator ) => string.Join(separator, enm) | some;
        public static somestr Concat(this IEnumerable<somestr> enm) => string.Concat(enm) | some;
    }
}