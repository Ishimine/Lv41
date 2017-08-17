using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnificadorDeCol2D))]
public class UnificadorDeCol2DEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UnificadorDeCol2D myTarget = (UnificadorDeCol2D)target;

        if (GUILayout.Button("Unificar PolygonCollider2D"))
        {
            myTarget.CrearColliderUnificado();
        }

        if (GUILayout.Button("Eliminar PolygonosHijos"))
        {
            myTarget.EliminarPolygonHijos();
        }


        if (GUILayout.Button("Deshabilitar PolygonosHijos"))
        {
            myTarget.DeshabilitarPolygonHijos();
        }


        if (GUILayout.Button("Habilitar PolygonosHijos"))
        {
            myTarget.ActivarPolygonHijos();
        }
    }
}
