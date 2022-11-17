using VDFramework;

namespace Utility
{
	public class ApplicationUtil : BetterMonoBehaviour
	{
		public void ExitApplication()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
			return;
#endif
			
			UnityEngine.Application.Quit();
		}
	}
}