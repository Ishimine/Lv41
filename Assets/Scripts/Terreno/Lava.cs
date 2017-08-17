using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Lava : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<EsferaJugador>().dead = true;
            StartCoroutine("ContadorCheck",col.gameObject);
        }
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = rb.velocity / 4;
            rb.gravityScale = 0.2f;
        }
    }



    IEnumerator ContadorCheck(GameObject player)
    {

        yield return new WaitForSeconds(.7f);
        StopCoroutine("ContadorCheck");

        player.GetComponent<EsferaJugador>().Dead();
    }




    IEnumerator ContadorReinicioNivel()
    {
        Debug.Log("GameOver");
        yield return new WaitForSeconds(.7f);
        StopCoroutine("ContadorReinicioNivel");

        GameController.ReiniciarNivel();
    }

    /*
    private void OnTriggerStay2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0.2f;
        }
    }*/

    private void OnTriggerExit2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1;
        }
    }
}
