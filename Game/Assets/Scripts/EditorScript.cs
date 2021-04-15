using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FloorManager))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FloorManager myScript = (FloorManager)target;
        if (GUILayout.Button("Build Dungeon"))
        {
            myScript.GenerateDungeon();
        }
    }
}