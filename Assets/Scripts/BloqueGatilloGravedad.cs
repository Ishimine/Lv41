using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(RotacionContinua))]
public class BloqueGatilloGravedad : MonoBehaviour
{

    public GameObject flecha;
    public Vector2 direccionGravedad;
    public SpriteRenderer indicador;
    private Color apagado = Color.gray;
    private Color encendido = Color.green;
    public RotacionContinua rot;


    private void Start()
    {
        rot = GetComponent<RotacionContinua>();
        DirectorGravedad.AgregarBoton(this);
        ActFlecha();
    }

    void ActFlecha()
    {
        Vector2 aux = direccionGravedad.normalized*-1;
        flecha.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(aux.y, aux.x) * Mathf.Rad2Deg));
    }

    private void OnValidate()
    {
        ActFlecha();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
           DirectorGravedad.CambiarGravedad(direccionGravedad);
        }
    }

   public void Encender()
    {
        indicador.color = encendido;
        rot.invertir = true;
    }

    public void Apagar()
    {
        indicador.color = apagado;
        rot.invertir = false;
    }


    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }
}
