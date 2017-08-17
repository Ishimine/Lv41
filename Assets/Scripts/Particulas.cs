using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
