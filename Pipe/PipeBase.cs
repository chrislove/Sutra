namespace SharpPipe
{
	public class PipeBase {
		protected bool IsInitialized { get; }= false;

		internal PipeBase() : this(false) {}

		internal PipeBase( bool isInitialized ) {
			IsInitialized = isInitialized;
		}
	}
}