using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texturaReferenciaSimple : MonoBehaviour {

    public Texture textura;


    void OnGUI()
    {
        if (!textura)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }
        GUI.DrawTexture(new Rect(10, 10, 60, 60), textura, ScaleMode.ScaleToFit, true, 10.0F);
    }
}
