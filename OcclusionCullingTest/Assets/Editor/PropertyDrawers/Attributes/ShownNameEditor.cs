using Attributes;
using UnityEditor;
using UnityEngine;
using VDFramework.Extensions;

namespace PropertyDrawers.Attributes
{
	[CustomPropertyDrawer(typeof(ShownNameAttribute))]
	public class ShownNameEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			string name = attribute.ConvertTo<ShownNameAttribute>().DisplayName;
			EditorGUI.PropertyField(position, property, new GUIContent(name));
		}
	}
}