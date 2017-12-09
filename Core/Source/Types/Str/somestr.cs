using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sutra.Transformations;
using static Sutra.CallerHelper;
using static Sutra.Commands;

namespace Sutra {
    /// <summary>
    /// Non-nullable string that is guaranteed to contain a value.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct somestr
    {
        private Option<string> _value;
        
        [NotNull]
        [JsonProperty]
        private string Value
            {
                get => _value.Match(i => i, () => throw new UninitializedSomeException("Trying to access uninitialized somestr"));
                set => _value = value;
            }


        public somestr( [NotNull] string value,
                        [CanBeNull] [CallerMemberName] string memberName = null,
                        [CanBeNull] [CallerFilePath] string filePath = null,
                        [CallerLineNumber] int lineNumber = 0 )
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidInputException($"Trying to create somestr from a null or empty string." + MakeCallerString(memberName, filePath, lineNumber));
                
                _value = value;
            }
        
        public somestr( IOption<string> str,
                        [CanBeNull] [CallerMemberName] string memberName = null,
                        [CanBeNull] [CallerFilePath] string filePath = null,
                        [CallerLineNumber] int lineNumber = 0 )
            {
                if (!str.HasValue)
                    throw new InvalidInputException($"Trying to create somestr from an empty option." + MakeCallerString(memberName, filePath, lineNumber));

                _value = str._value();
            }

        [NotNull] [PublicAPI]
        public string _ => Value;

        public char this[ int i ]
            {
                get
                    {
                        if (i < 0 || i >= Value.Length)
                            throw new InvalidOperationException($"Invalid string indexing operation: {i} is out of range [0..{Value.Length}]");
                            
                        return Value[i];
                    }
            }

        public Option<U> Map<U>( [NotNull] Func<string, U> func ) => func(Value).ToOption();
        public str Map( [NotNull] Func<string, string> func ) => func(Value);
        public str Map( [NotNull] Func<somestr, somestr> func ) => func(Value);

        public override string ToString() => Value;

        public static somestr From([NotNull] string value) => new somestr(value);
        public static somestr From(str value) => new somestr(value);

        public static implicit operator string( somestr str ) => str.Value;
        public static implicit operator Some<string>( somestr str ) => new Some<string>(str);
        public static implicit operator somestr( Some<string> some ) => new somestr(some);
        
        /// <summary>
        /// Unsafe, will throw if we're trying to create somestr from a null/empty string.
        /// </summary>
        public static implicit operator somestr( [NotNull] string str ) => new somestr(str);

        public static somestr operator +( somestr left, [NotNull] string right ) => left._ + right;
        public static somestr operator +( somestr left, str right ) => left + right.ValueOr("");
        public static somestr operator +( somestr left, somestr right ) => left._ + right._ | some;
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static somestr operator |( [CanBeNull] string a, somestr b ) => a | opt | b;

        /// <summary>
        /// Returns the string contained within the somestr.
        /// </summary>
        [NotNull]
        public static string operator |( somestr a, DoGet _ ) => a.Value;

        public bool Equals( somestr other ) => string.Equals(Value, other.Value);

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is somestr && Equals((somestr) obj);
            }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==( somestr left, somestr right ) => left.Equals(right);
        public static bool operator !=( somestr left, somestr right ) => !left.Equals(right);

        public static bool operator ==( somestr left, str right ) => right.HasValue ? left.Equals(right | some)  : false;
        public static bool operator !=( somestr left, str right ) => right.HasValue ? !left.Equals(right | some) : true;

        public static bool operator ==( somestr left, string right ) => left == (right | opt);
        public static bool operator !=( somestr left, string right ) => left != (right | opt);
    }
}