using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapEditorTool))]
public class MapEditorToolEditor : Editor {
    private SerializedProperty tilePrefabs;
    private SerializedProperty parentContainer;
    private SerializedProperty activePrefabIndex;

    private void OnEnable() {
        tilePrefabs = serializedObject.FindProperty("tilePrefabs");
        parentContainer = serializedObject.FindProperty("parentContainer");
        activePrefabIndex = serializedObject.FindProperty("activePrefabIndex");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(tilePrefabs, true);
        EditorGUILayout.PropertyField(parentContainer);

        // Draw prefab palette buttons
        MapEditorTool tool = (MapEditorTool)target;
        if (tool.tilePrefabs != null && tool.tilePrefabs.Length > 0) {
            EditorGUILayout.LabelField("Tile Palette", EditorStyles.boldLabel);

            for (int i = 0; i < tool.tilePrefabs.Length; i++) {
                GUI.backgroundColor = (i == tool.activePrefabIndex) ? Color.green : Color.white;

                if (GUILayout.Button(tool.tilePrefabs[i] != null ? tool.tilePrefabs[i].name : "Empty")) {
                    activePrefabIndex.intValue = i;
                }
            }

            GUI.backgroundColor = Color.white;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI() {
        MapEditorTool tool = (MapEditorTool)target;

        Event e = Event.current;

        // Create a ray from the mouse into the scene
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        // Define a plane at y = 0 (the ground)
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter)) {
            Vector3 hitPoint = ray.GetPoint(enter);

            // Snap to integer grid
            int x = Mathf.FloorToInt(hitPoint.x);
            int z = Mathf.FloorToInt(hitPoint.z);
            Vector3 snappedPos = new Vector3(x, 0, z);

            // Draw preview square on grid
            Handles.color = Color.green;
            Handles.DrawSolidRectangleWithOutline(
                new Rect(new Vector2(snappedPos.x, snappedPos.z), Vector2.one),
                new Color(0, 1, 0, 0.1f),
                Color.green
            );

            // Left-click to place
            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt) {
                e.Use();
                tool.PlaceTile(x, z);
            }

            // Right-click to remove
            if (e.type == EventType.MouseDown && e.button == 1 && !e.alt) {
                e.Use();
                tool.RemoveTile(x, z);
            }
        }
    }
}
