using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UI.UsefullButton))]
[CanEditMultipleObjects]
public class UsefullButtonEditor : UnityEditor.UI.ButtonEditor {

    private SerializedProperty buttonText;

    protected override void OnEnable() {
        buttonText = serializedObject.FindProperty("buttonText");
        base.OnEnable();
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.LabelField("Button parameters");
        base.OnInspectorGUI();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Usefull parameters");
        EditorGUILayout.PropertyField(buttonText, new GUIContent(buttonText.displayName, buttonText.tooltip));
        if (GUI.changed) {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
