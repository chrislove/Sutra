using JetBrains.Annotations;

namespace SharpPipe
{
	public struct DoEnd {}

	public struct DoConcat {
		[CanBeNull] internal readonly string Separator;
		
		public DoConcat( [CanBeNull] string separator ) {
			Separator = separator;
		}
	}
	
	public struct DoToList {}
	public struct DoToArray {}
}