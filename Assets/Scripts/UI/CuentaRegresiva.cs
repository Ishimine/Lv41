using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CuentaRegresiva : MonoBehaviour {
    
    public Animator anim;
    public SpriteRenderer img;
    
    
    public void IniciarCuentaRegresiva()
    {
        StartCoroutine(CuentaRegresivaInicio());
    }

    private void OnDestroy()
    {
        GameController.PreSesionDeJuego -= IniciarCuentaRegresiva;
    }

    IEnumerator CuentaRegresivaInicio()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        anim.SetTrigger("Activar");
        yield return new WaitForSecondsRealtime(.9f);
        anim.SetTrigger("Activar");
        yield return new WaitForSecondsRealtime(1);
        anim.SetTrigger("Activar");
        yield return new WaitForSecondsRealtime(1);
        anim.SetTrigger("Activar Final");
    }


    
}
