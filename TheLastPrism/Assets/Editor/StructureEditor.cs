using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Structure), true)]
[CanEditMultipleObjects]
public class StructureEditor : Editor
{
    SerializedProperty itemList;
    private Structure structure;

    private void OnEnable()
    {
        structure = (Structure) target;
        itemList = serializedObject.FindProperty("dropItemList");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Tool"))
        {
            structure.DropItemList.Add(new ItemTool(null, 1, 0));
        }
        if (GUILayout.Button("Add Material"))
        {
            structure.DropItemList.Add(new ItemMaterial(null, 1));
        }
        if (GUILayout.Button("Add Block"))
        {
            structure.DropItemList.Add(new ItemBlock(null, 1));
        }
        if (GUILayout.Button("Add Structure"))
        {
            structure.DropItemList.Add(new ItemBlock(null, 1));
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}