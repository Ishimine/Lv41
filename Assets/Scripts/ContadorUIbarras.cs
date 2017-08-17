using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorUIbarras : MonoBehaviour
{

    Text txt;

    public void Restart()
    {
        txt.text = "0";
    }

    void Awake()
    {
        txt = GetComponent<Text>();
        txt.text = "0";
        FindObjectOfType<TouchControl>().bCreada += ActualizarTexto;
    }


    void ActualizarTexto(int n)
    {
        if (txt != null)
            txt.text = n.ToString();

    }
}
