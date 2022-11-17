using UnityEngine;

namespace Analysis
{
	public class SingleProfilerFileRecorder : ProfilerUtil
	{
		[SerializeField]
		private string fileName = "PROFILE.raw";
		
		// Animation system does not support overloaded functions
		public void StartRecordingData()
		{
			StartProfiling(fileName);
		}
	}
}