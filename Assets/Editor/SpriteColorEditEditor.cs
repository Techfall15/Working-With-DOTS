using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteColorEditAuthoring))]
[CanEditMultipleObjects]
public class SpriteColorEditEditor : Editor
{
    SerializedProperty m_SpriteColor;
    SerializedProperty m_randomizeColor;
    public void OnEnable()
    {
        m_SpriteColor = serializedObject.FindProperty("spriteColor");
        m_randomizeColor = serializedObject.FindProperty("randomizeColor");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SpriteColorEditAuthoring author = (SpriteColorEditAuthoring)target;

        EditorGUILayout.PropertyField(m_SpriteColor);
        EditorGUILayout.PropertyField (m_randomizeColor);


        author.UpdateSpriteColor(m_SpriteColor.colorValue);
        serializedObject.ApplyModifiedProperties();
    }
}
