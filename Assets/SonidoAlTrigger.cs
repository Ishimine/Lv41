using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoAlTrigger : MonoBehaviour {


    public AudioSource audio;


    public void Desactivar()
    {
        Destroy(this);
    }
    private void Awake()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.Play();
        }
    }
}
