using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

[RequireComponent(typeof(ZST_SmartTile))]
[RequireComponent(typeof(PolygonCollider2D))]
public class ColliderSmart : MonoBehaviour {

    PolygonCollider2D col;
    ZST_SmartTile zSTile;
    float tamaño;


    private void Awake()
    {
        col = GetComponent<PolygonCollider2D>();
        zSTile = GetComponent<ZST_SmartTile>();
        ActualizarColision();

    }




    void Reset()
    {
        if (col == null)    
            col = GetComponent<PolygonCollider2D>();
        if(zSTile == null)
            zSTile = GetComponent<ZST_SmartTile>();
        ActualizarColision();
    }


    void ActualizarColision()
    {
        tamaño = zSTile.sideLengthInPixels/100;
        Vector2[] vec = new Vector2[4];

        vec[0] = new Vector2(-tamaño / 2, tamaño / 2);
        vec[1] = new Vector2(tamaño / 2, tamaño / 2);
        vec[2] = new Vector2(tamaño / 2, -tamaño / 2);
        vec[3] = new Vector2(-tamaño / 2, -tamaño / 2);

        col.SetPath(0,vec);
    }

}
