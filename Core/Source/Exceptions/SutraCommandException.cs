namespace Sutra {
    internal class SutraCommandException : SutraException {
        public SutraCommandException( string commandName ) : base($"{commandName} command threw an exception.") { }
    }
}