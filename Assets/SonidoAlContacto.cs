using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoAlContacto : MonoBehaviour {

    public bool variarPitch = false;
    public float rango = 0.2f;
    public float centro = 1f;



    public AudioSource audio;

    private void Awake()
    {
        if(audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(variarPitch)
            {
                audio.pitch = Random.Range(centro - rango, centro + rango);
            }
            audio.Play();
        }
    }
}
