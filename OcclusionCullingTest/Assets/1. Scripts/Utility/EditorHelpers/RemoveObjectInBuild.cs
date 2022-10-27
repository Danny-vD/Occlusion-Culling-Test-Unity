using VDFramework;

namespace Utility.EditorHelpers
{
	public class RemoveObjectInBuild : BetterMonoBehaviour
	{
#if !UNITY_EDITOR
		private void Awake()
		{
				Destroy(gameObject);
		}
#endif
	}
}