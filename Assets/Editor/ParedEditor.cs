using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pared))]
public class ParedEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Pared myTarget = (Pared)target;

        if (GUILayout.Button("Crear Pared"))
        {
            myTarget.CrearPared();
        }        
    }
}
