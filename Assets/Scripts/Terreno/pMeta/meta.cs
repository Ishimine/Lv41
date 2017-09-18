using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meta : MonoBehaviour {

    public SpriteRenderer render;
    public ControlParticula p;
    Vector2 vel;
    public Color colorFinal;

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.GetComponent<EsferaJugador>().dead)
                return;

            StartCoroutine(CentrarJugador(other.transform));
            other.GetComponent<EsferaJugador>().EnMeta();
            GameController.FinNivel();
            CambiarColorBorde();
            if (p != null) p.CrearBurst(1);


            //ShakeCam
            ShakeControl.instance.activado = true;
            ShakeControl.instance.ActivarShake(ShakeControl.FuerzaShake.Medio);
            ShakeControl.instance.activado = false;

        }
    }


    IEnumerator CentrarJugador(Transform other)
    {
        while(Vector2.Distance(transform.position, other.position) > 0.05)
        {
            other.position = Vector2.SmoothDamp(other.position, transform.position, ref vel, 0.3f, 10, Time.deltaTime);
            yield return null;
        }
    }

    void CambiarColorBorde()
    {
        if(render != null)
        {
            render.color = colorFinal;
        }
    }


}
