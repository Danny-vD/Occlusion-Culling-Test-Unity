namespace Analysis
{
	public class SingleProfilerFileRecorder : ProfilerFileRecorder
	{
		// Animation system does not support overloaded functions
		public void StartRecordingData()
		{
			StartProfiling(CachedGameObject.name);
		}
	}
}