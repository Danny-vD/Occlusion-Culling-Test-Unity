namespace Analysis
{
	public class SingleProfilerFileRecorder : ProfilerUtil
	{
		// Animation system does not support overloaded functions
		public void StartRecordingData()
		{
			StartProfiling(CachedGameObject.name);
		}
	}
}