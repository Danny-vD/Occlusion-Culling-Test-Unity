using System.IO;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using VDFramework;

namespace Analysis
{
	public class ProfilerFileRecorder : BetterMonoBehaviour
	{
		private byte recordingID = 0;

		private string filePath = "Profiles/";
		private string sceneName = "CurrentScene";

		private bool occlusionCullingEnabled;

		private string OcclusionCulling => occlusionCullingEnabled ? "ON" : "OFF";

		private void Awake()
		{
			recordingID      = 0;
			Profiler.enabled = false;
			sceneName        = SceneManager.GetActiveScene().name;

			occlusionCullingEnabled = Camera.main.useOcclusionCulling;

			filePath += $"{sceneName}[{OcclusionCulling}]/";

			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
		}

		public void StartProfiling(string logName)
		{
			++recordingID;
			Profiler.logFile         = filePath + $"{recordingID}_{logName}";
			Profiler.enableBinaryLog = true;

			Profiler.enabled = true;

#if UNITY_EDITOR

			// Apparently needed in the editor if you don't have the profiler window open
			UnityEditorInternal.ProfilerDriver.enabled = true;
#endif
		}

		public void EndProfiling()
		{
#if UNITY_EDITOR

			// Apparently needed in the editor if you don't have the profiler window open
			UnityEditorInternal.ProfilerDriver.enabled = false;
#endif

			Profiler.enabled         = false;
			Profiler.enableBinaryLog = false;
			Profiler.logFile         = string.Empty;
		}

		private void OnDestroy()
		{
			EndProfiling();
		}
	}
}