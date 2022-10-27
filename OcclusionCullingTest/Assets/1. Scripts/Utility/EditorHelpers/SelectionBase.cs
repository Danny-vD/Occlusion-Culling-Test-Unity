using UnityEngine;
using VDFramework;

namespace Utility.EditorHelpers
{
	[SelectionBase]
	public class SelectionBase : BetterMonoBehaviour
	{
#if !UNITY_EDITOR

		// Remove this class in a build
		private void Start()
		{
			Destroy(this);
		}
#endif
	}
}