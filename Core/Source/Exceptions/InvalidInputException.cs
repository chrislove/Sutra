using System;
using JetBrains.Annotations;

namespace Sutra
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException([CanBeNull] string message) : base(message) { }
    }
}