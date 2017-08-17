using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pared : MonoBehaviour
{
    public bool invertirX = false;
    public bool invertirY = false;

    public bool unificarBloques = false;
    public GameObject bloque;
    public int cantDeBloques = 3;
    public Vector2 tamañoBloques;
    public bool limpiarAlCrear = true;
    public Vector2 dirDist;
    public bool crearDesdeCentro = true;


    public Sprite spriteBloque;
    public Texture2D texturaBloque;

    void ActTamaño()
    {
      // tamañoBloques = bloque.GetComponent<PropiedadesMat>().tamaño/100;
    }

    void ActSprite()
    {
        spriteBloque = bloque.GetComponent<SpriteRenderer>().sprite;
    }

    void ActTextura()
    {
        texturaBloque = bloque.GetComponent<SpriteRenderer>().sprite.texture;
    }

    public void CrearPared()
    {

        //Crea el objeto padre "Pared"
        GameObject pared = new GameObject();
        pared.transform.parent = transform.parent;
        pared.transform.position = transform.position;
        pared.name = "Pared";
        pared.AddComponent<UnificadorDeCol2D>();

        ActTamaño();
        if (bloque == null)
        {
            Debug.Log("Pared vacia");
            return;
        }


        if (limpiarAlCrear)             //Limpia pared previa
            Limpiar();




        Vector2 centro = new Vector2(0, 0);
        if (crearDesdeCentro)
        {
            centro = getDesplazamientoDeCentro();
        }

        Vector2 desplazamiento = centro + (Vector2)transform.position;

        //Vector2 dir = new Vector2(Mathf.Sign(dirDist.x), Mathf.Sign(dirDist.y));

        PropiedadesMat pro = bloque.GetComponent<PropiedadesMat>();
        for (int i = 0; i < cantDeBloques; i++)
        {
            GameObject nuevo = Instantiate<GameObject>(bloque, pared.transform);
            desplazamiento += new Vector2(dirDist.x * tamañoBloques.x, dirDist.y * tamañoBloques.y);
            nuevo.transform.position = desplazamiento;


            InvertirBloque(nuevo.transform);
        }



        if (unificarBloques)//Unifica los colliders y luego los elimina
        {
            pared.GetComponent<UnificadorDeCol2D>().CrearColliderUnificado();
            pared.GetComponent<UnificadorDeCol2D>().EliminarPolygonHijos();

            //Aplica las propiedades del material
            GameObject x = pared.GetComponent<UnificadorDeCol2D>().cuerpo;
            x.AddComponent<PropiedadesMat>();
            //x.GetComponent<PropiedadesMat>().tamaño = new Vector2(pro.tamaño.x + (pro.tamaño.x * (cantDeBloques - 1) * dirDist.x), pro.tamaño.y + (pro.tamaño.y * (cantDeBloques - 1) * dirDist.y));
            x.GetComponent<PropiedadesMat>().indiceRebote = pro.indiceRebote;
        }

    }

    private void InvertirBloque(Transform nuevo)
    {
        Vector2 nuevaEscala = nuevo.localScale;
        if (invertirX)
            nuevaEscala.x = nuevo.localScale.x * (-1);
        if (invertirY)
            nuevaEscala.y = nuevo.localScale.y * (-1);

        nuevo.localScale = nuevaEscala;
    } //En caso de estar activada la inversion se invierten las piezas

    private Vector2 getDesplazamientoDeCentro()
    {
       return new Vector2(dirDist.x * tamañoBloques.x * cantDeBloques / 2 * (-1), dirDist.y * tamañoBloques.y * cantDeBloques / 2 * (-1)) - tamañoBloques/2;
    }    

    bool checkBloqueEsValido()
    {
        if (bloque == null || bloque.GetComponent<PropiedadesMat>() == null)
        {
            Debug.Log("Cuidado Bloque invalido: " + gameObject.name);
            bloque = null;
            return false;
        }
        return true;
    }


    private void OnValidate()
    {
        if(checkBloqueEsValido())
        {
            ActTamaño();
            ActSprite();
            ActTextura();
        }
    }


    public void Limpiar()
    {
        int a = transform.childCount-1;
        for (int i = a; i > -1;i--)
        {
             DestroyImmediate(transform.GetChild(i).gameObject);
        }
    
    }


    /* void OnDrawGizmos()
     {
         Gizmos.color = Color.yellow;
         Rect res = bloque.GetComponent<SpriteRenderer>().sprite.textureRect;
         res.height /= 100;
         res.width /= 100;
         Texture img = bloque.GetComponent<SpriteRenderer>().sprite.texture;


         Vector2 centro = Vector2.zero;
         if (crearDesdeCentro)
         {
             centro = getCentro();
         }
         Vector2 desplazamiento = centro + (Vector2)transform.position;

         for (int i = 0; i< cantDeBloques;i++)
         {
             Vector2 pos = desplazamiento + new Vector2((dirDist.x * tamañoBloques.x * i)-tamañoBloques.x/2, (dirDist.y * tamañoBloques.y * i)-tamañoBloques.y/2);
             res.position = pos;
             Gizmos.DrawGUITexture(res, img);
         }
     }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 desplazamiento = getDesplazamientoDeCentro() + (Vector2)transform.position;
        Vector3 pos = desplazamiento;
        for (int i = 0; i < cantDeBloques; i++)
        {
            pos += new Vector3(tamañoBloques.x *dirDist.x, tamañoBloques.y*dirDist.y);
            Gizmos.DrawWireCube(pos,tamañoBloques);
        }




        Rect res = spriteBloque.rect;
        res.position = (Vector2)transform.position - new Vector2(tamañoBloques.x,tamañoBloques.y*-1)/2;
        res.size = new Vector2(res.size.x,res.size.y*-1)/100;

        if (invertirX)
            res.width *= -1;

        if (invertirY)
            res.height *= -1;

        Gizmos.DrawGUITexture(res, texturaBloque);


        



        /*
        Rect posFlechaX = new Rect(0, -flecha.texture.height / 2, 5, 5);
        if (invertirX)
            posFlechaX.height *= -1;


        Rect posFlechaY = new Rect(-flecha.texture.width/2, 0, 5, 5);
        if (invertirY)
            posFlechaY.width *= -1;


        posFlechaX.position = (Vector2)transform.position + Vector2.up*8;
        posFlechaY.position = (Vector2)transform.position + Vector2.down*8;

        Gizmos.DrawGUITexture(posFlechaX, flecha.texture);
        Gizmos.DrawGUITexture(posFlechaY, flecha.texture);
        */


    }
}
