using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meta : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector2 vel;
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.GetComponent<EsferaJugador>().dead)
                return;
            StartCoroutine(CentrarJugador(other.transform));
            other.GetComponent<EsferaJugador>().EnMeta();
            GameController.FinNivel();
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




}
