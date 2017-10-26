namespace SharpPipe {
    public static class CallerHelper
    {
        public static somestr MakeCallerString( string memberName, string filePath, int lineNumber )
            => $" Called from {filePath}:{lineNumber} {memberName}" | Commands.some;
    }
}