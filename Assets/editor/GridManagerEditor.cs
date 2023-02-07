using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        GridManager gridManager = (GridManager)target;
        if (GUILayout.Button("Create Grid"))
        {
            gridManager.SpawnOrUpdateGrid(); // how do i call this?
        }
        DrawDefaultInspector();
    }
}
