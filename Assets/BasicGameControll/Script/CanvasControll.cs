using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControll : MonoBehaviour
{

    public Animator anim;

    
    public void MenuPrincipal(bool x)
    {
        anim.SetBool("MenuPrincipal",x);
    }

    public void Pausa(bool x)
    {
        anim.SetBool("Pausa", x);
    }
}
