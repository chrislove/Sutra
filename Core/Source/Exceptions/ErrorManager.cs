using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    public static class ErrorManager
    {
        /// <summary>
        /// When set to true the exception will be thrown on 'get' command.
        /// </summary>
        public static bool DelayedThrow = false;

        [ThreadStatic]
        private static Stack<Exception> _errorStack;

        [NotNull]
        private static Stack<Exception> ErrorStack => _errorStack ?? (_errorStack = new Stack<Exception>());
    }
}