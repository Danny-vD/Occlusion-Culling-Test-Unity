using UnityEngine;
using VDFramework;

namespace Utility.EditorHelpers
{
	/// <summary>
	/// A utility class that has a text area to write a comment in
	/// </summary>
	public class Comment : BetterMonoBehaviour
	{
		[SerializeField, TextArea(3, 8)]
		private string comment;

#if !UNITY_EDITOR

		// Remove this class in a build
		private void Awake()
		{
			Destroy(this);
		}
#endif
	}
}