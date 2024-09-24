using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class SelectTransforms : EditorWindow
{
    Vector3 scale;
    bool extra;
    string name;
    string parentHas;
    bool scaling;

    [MenuItem("Baldi/Select Transforms")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SelectTransforms));
    }

    void OnGUI()
    {
        scaling = EditorGUILayout.Toggle("Scaling Check?", scaling);
        if (scaling)
            scale = EditorGUILayout.Vector3Field("Scale:", scale);
        name = EditorGUILayout.TextField("Forced Name:", name);
        parentHas = EditorGUILayout.TextField("Parent Has X in Name:", parentHas);
        extra = EditorGUILayout.Toggle("Force Mesh Renderers", extra);
        EditorGUILayout.Space();
        if (GUILayout.Button("Select Transforms"))
        {
            List<GameObject> toSelect = new List<GameObject>();
            foreach (Transform t in FindObjectsOfType<Transform>())
            {
                if (scaling)
                    if (t.localScale != scale) continue;
                if (name != string.Empty)
                    if (t.gameObject.name != name) continue;
                if (parentHas != string.Empty && t.parent != null)
                    if (!t.parent.gameObject.name.Contains(parentHas)) continue;
                if (!extra || t.GetComponent<MeshRenderer>())
                    toSelect.Add(t.gameObject);
            }
            Selection.objects = toSelect.ToArray();
        }
    }

    private void Awake()
    {
        scale = Vector3.one;
    }
}
