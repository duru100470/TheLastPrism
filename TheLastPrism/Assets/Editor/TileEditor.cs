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
        if (GUILayout.Button("Add Item"))
        {
            tile.DropItemList.Add(new Item());
        }
        if (GUILayout.Button("Add Tool"))
        {
            tile.DropItemList.Add(new Tool());
        }
        serializedObject.ApplyModifiedProperties();
    }
}