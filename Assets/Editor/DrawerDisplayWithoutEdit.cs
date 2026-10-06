using UnityEditor;
using UnityEngine;

/*
 * Code copied from: https://discussions.unity.com/t/showing-an-array-with-enum-as-keys-in-the-property-inspector/218840/3
 */

[CustomPropertyDrawer(typeof(DisplayWithoutEdit))]
public class DrawerDisplayWithoutEdit : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}