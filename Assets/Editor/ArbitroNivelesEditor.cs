using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArbitroNiveles))]
public class ArbitroNivelesEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ArbitroNiveles myTarget = (ArbitroNiveles)target;

        if (GUILayout.Button("Guardar Records"))
        {
            myTarget.GuardarRecords();  
        }        
        if (GUILayout.Button("Cargar Records"))
        {
            myTarget.CargarRecords();
        }


        if (GUILayout.Button("Guardar Objetivos"))
        {
            myTarget.GuardarObjetivos();
        }
        if (GUILayout.Button("Cargar Objetivos"))
        {
            myTarget.CargarObjetivos();
        }

        if (GUILayout.Button("Resetear Records"))
        {
            myTarget.ResetRecords();
        }

        DrawDefaultInspector();

    }
}
