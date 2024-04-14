using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotationOverTimeAuthoring))]
[CanEditMultipleObjects]
public class RotationOverTimeEditor : Editor
{
    SerializedProperty m_RotationSpeed;
    SerializedProperty m_RotateClockWise;
    SerializedProperty m_RandomizeDirection;
    SerializedProperty randomizeSpeed;
    SerializedProperty minMax;
    public void OnEnable()
    {
        m_RotationSpeed      = serializedObject.FindProperty("rotationSpeed");
        m_RotateClockWise    = serializedObject.FindProperty("clockwiseRotation");
        m_RandomizeDirection = serializedObject.FindProperty("randomizeDirection");
        randomizeSpeed       = serializedObject.FindProperty("randomizeSpeed");
        minMax               = serializedObject.FindProperty("minMax");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_RotationSpeed);
        EditorGUILayout.PropertyField(m_RotateClockWise);
        EditorGUILayout.PropertyField(m_RandomizeDirection);
        EditorGUILayout.PropertyField(randomizeSpeed);
        EditorGUI.BeginDisabledGroup(randomizeSpeed.boolValue == false);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(minMax);
        EditorGUI.indentLevel--;
        EditorGUI.EndDisabledGroup();
        

        serializedObject.ApplyModifiedProperties();
    }


}
