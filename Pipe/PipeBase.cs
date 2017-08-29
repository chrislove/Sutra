namespace SharpPipe
{
	public abstract class PipeBase {
		protected bool IsInitialized { get; }= false;

		protected PipeBase( bool isInitialized ) {
			IsInitialized = isInitialized;
		}
	}
}