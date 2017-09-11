

/*
namespace SharpPipe {
    public partial class Curry {
        public abstract class DATETIME { }
    }

    public static class IDateTimeA {
        public static Func<DateTime, DateTime> Date => i => i.Date;
        public static PipeFunc<DateTime, int> Day => From(i => i.Day);
        public static PipeFunc<DateTime, DayOfWeek> DayOfWeek => i => i.DayOfWeek;
        public static PipeFunc<DateTime, int> DayOfYear => i => i.DayOfYear;
        public static PipeFunc<DateTime, int> Hour => i => i.Hour;
        public static PipeFunc<DateTime, DateTimeKind> Kind => i => i.Kind;
        public static PipeFunc<DateTime, int> Millisecond => i => i.Millisecond;
        public static PipeFunc<DateTime, int> Minute => i => i.Minute;
        public static PipeFunc<DateTime, int> Month => i => i.Month;
        public static PipeFunc<DateTime, int> Second => i => i.Second;
        public static PipeFunc<DateTime, long> Ticks => i => i.Ticks;
        public static PipeFunc<DateTime, TimeSpan> TimeOfDay => i => i.TimeOfDay;
        public static PipeFunc<DateTime, int> Year => i => i.Year;
        public static Func<DateTime, DateTime> Add( TimeSpan value ) => i => i.Add(value);
        public static Func<DateTime, DateTime> AddDays( double value ) => i => i.AddDays(value);
        public static Func<DateTime, DateTime> AddHours( double value ) => i => i.AddHours(value);
        public static Func<DateTime, DateTime> AddMilliseconds( double value ) => i => i.AddMilliseconds(value);
        public static Func<DateTime, DateTime> AddMinutes( double value ) => i => i.AddMinutes(value);
        public static Func<DateTime, DateTime> AddMonths( int months ) => i => i.AddMonths(months);
        public static Func<DateTime, DateTime> AddSeconds( double value ) => i => i.AddSeconds(value);
        public static Func<DateTime, DateTime> AddTicks( long value ) => i => i.AddTicks(value);
        public static Func<DateTime, DateTime> AddYears( int value ) => i => i.AddYears(value);
        public static PipeFunc<DateTime, int> CompareTo( DateTime value ) => i => i.CompareTo(value);
        public static PipeFunc<DateTime, bool> Equals( DateTime value ) => i => i.Equals(value);
        public static PipeFunc<DateTime, bool> IsDaylightSavingTime => i => i.IsDaylightSavingTime();
        public static PipeFunc<DateTime, long> ToBinary => i => i.ToBinary();
        public static PipeFunc<DateTime, int> GetHashCode => i => i.GetHashCode();
        public static PipeFunc<DateTime, TimeSpan> Subtract( DateTime value ) => i => i.Subtract(value);
        public static Func<DateTime, DateTime> Subtract( TimeSpan value ) => i => i.Subtract(value);
        public static PipeFunc<DateTime, double> ToOADate => i => i.ToOADate();
        public static PipeFunc<DateTime, long> ToFileTime => i => i.ToFileTime();
        public static PipeFunc<DateTime, long> ToFileTimeUtc => i => i.ToFileTimeUtc();
        public static Func<DateTime, DateTime> ToLocalTime => i => i.ToLocalTime();
        public static PipeFunc<DateTime, string> ToLongDateString => i => i.ToLongDateString();
        public static PipeFunc<DateTime, string> ToLongTimeString => i => i.ToLongTimeString();
        public static PipeFunc<DateTime, string> ToShortDateString => i => i.ToShortDateString();
        public static PipeFunc<DateTime, string> ToShortTimeString => i => i.ToShortTimeString();
        public static PipeFunc<DateTime, string> ToString() => i => i.ToString();
        public static PipeFunc<DateTime, string> ToString( string format ) => i => i.ToString(format);
        public static PipeFunc<DateTime, string> ToString( IFormatProvider provider ) => i => i.ToString(provider);
        public static PipeFunc<DateTime, string> ToString( string format, IFormatProvider provider ) => i => i.ToString(format, provider);
        public static Func<DateTime, DateTime> ToUniversalTime => i => i.ToUniversalTime();
        public static ToSeqFunc<DateTime, string[]> GetDateTimeFormats() => i => i.GetDateTimeFormats();
        public static ToSeqFunc<DateTime, string[]> GetDateTimeFormats( IFormatProvider provider ) => i => i.GetDateTimeFormats(provider);
        public static ToSeqFunc<DateTime, string[]> GetDateTimeFormats( char format ) => i => i.GetDateTimeFormats(format);
        public static ToSeqFunc<DateTime, string[]> GetDateTimeFormats( char format, IFormatProvider provider ) => i => i.GetDateTimeFormats(format, provider);
        public static PipeFunc<DateTime, TypeCode> GetTypeCode => i => i.GetTypeCode();
    }

    public static class IDateTimeStatic {
        public static DateTime Now => i => i.
        public static DateTime UtcNow => i => i.
        public static DateTime Today => i => i.
        public static int Compare( DateTime t1, DateTime t2 );
        public static int DaysInMonth( int year, int month );
        public static bool Equals( DateTime t1, DateTime t2 );
        public static DateTime FromBinary( long dateData );
        public static DateTime FromFileTime( long fileTime );
        public static DateTime FromFileTimeUtc( long fileTime );
        public static DateTime FromOADate( double d );
        public static DateTime SpecifyKind( DateTime value, DateTimeKind kind );
        public static bool IsLeapYear( int year );
        public static DateTime Parse( string s );
        public static DateTime Parse( string s, IFormatProvider provider );
        public static DateTime Parse( string s, IFormatProvider provider, DateTimeStyles styles );
        public static DateTime ParseExact( string s, string format, IFormatProvider provider );
        public static DateTime ParseExact( string s, string format, IFormatProvider provider, DateTimeStyles style );
        public static DateTime ParseExact( string s, string[] formats, IFormatProvider provider, DateTimeStyles style );
        public static bool TryParse( string s, out DateTime result );
        public static bool TryParse( string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result );
        public static bool TryParseExact( string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result );
        public static bool TryParseExact( string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result );
    }
}*/