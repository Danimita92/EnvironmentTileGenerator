using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(EnvTileGenerator)), CanEditMultipleObjects, ExecuteInEditMode]
public class EnvTileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnvTileGenerator myScript = (EnvTileGenerator)target;

        if (GUILayout.Button("Generate Tiles")) //"Generate Tiles" button
        {
            myScript.GenerateTiles();
        }

        if (GUILayout.Button("Delete Tiles")) //"Delete Tiles" button
        {
            myScript.DeleteTiles();
        }
    }
}
