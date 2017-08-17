using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(contenedorDeTerreno))]
public class contenedorDeTerrenoEditor : Editor {


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        contenedorDeTerreno myTarget = (contenedorDeTerreno)target;

        if (GUILayout.Button("Eliminar Contenedor"))
        {
            myTarget.EliminarContenedor();
        }

    }
}
