using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraJugador : MonoBehaviour {

    public AudioSource audio;

  

    public void OnEnable()
    {
        audio.pitch = Random.Range(1.2f, 1.2f);
        audio.Play();
    }
}
