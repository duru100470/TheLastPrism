using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile), true)]
[CanEditMultipleObjects]
public class TileEditor : Editor
{
    SerializedProperty itemList;
    private Tile tile;

    private void OnEnable()
    {
        tile = (Tile) target;
        itemList = serializedObject.FindProperty("dropItemList");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Tool"))
        {
            tile.DropItemList.Add(new ItemTool(null, 1, 0));
        }
        if (GUILayout.Button("Add Material"))
        {
            tile.DropItemList.Add(new ItemMaterial(null, 1));
        }
        if (GUILayout.Button("Add Block"))
        {
            tile.DropItemList.Add(new ItemBlock(null, 1));
        }
        if (GUILayout.Button("Add Structure"))
        {
            tile.DropItemList.Add(new ItemStructure(null, 1));
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}