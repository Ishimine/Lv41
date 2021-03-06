﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaJugador : MonoBehaviour {

    /// <summary>
    /// Si es False, solo rebotara contra objetos que tengan el componente PropiedadesMat. Caso contrario con objetos sin Propiedades Mat
    /// rebotara igual al fRebote
    /// </summary>

    public SonidosPorArreglo audios;
    public CuentaRegresiva cuenta;
    public bool animar = false;
    public bool rebotarSiempre;
    public bool usarSistViejo;
    public bool usarUnicoContacto;

    public GameObject trail;
    
    public Collider2D col;
    public Rigidbody2D rb;

    private int activePower = 0;


    public delegate void gatillo();
    public event gatillo muerto;

    public ControlParticula particulasImpacto;
    public ControlParticula particulasMuerte;
    public int cantParticulas = 30;

    public float fRebote = 10;

    public Animator anim;

    bool rebote = false;

    public bool dead = false;

    public void desactivarTrail()
    {
        trail.SetActive(false);
    }
    public void activarTrail()
    {
        trail.SetActive(true);
    }

    private void Start()
    {
        InicializarJugador();
        dead = false;
    }

    void InicializarJugador()
    {
        if (anim != null) anim.SetBool("Vivo",true);
        trail.SetActive(true);

        //Aplicar Skin
        //ReiniciarVida
    }

    public void RevivirJugador()
    {
        col.isTrigger = false;

        rb.simulated = true;
        rb.isKinematic = false;
        dead = false;
        if (anim != null) anim.SetBool("Vivo", true);
    }

    public void EnMeta()
    {
        deshabilitar();
        if (anim != null && animar)
            anim.SetTrigger("Meta");
    }

    public void deshabilitar()
    {
        //print("Desabilitada");
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        col.isTrigger = true;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!this.isActiveAndEnabled)
        {
            return;
        }
        if (usarSistViejo)
        {
            Vector2 dir = (Vector2)transform.position - other.contacts[0].point;
            rb.velocity = dir * fRebote;
        }
        else if (!usarUnicoContacto)
        {
            foreach (ContactPoint2D cont in other.contacts)
            {
                Vector2 contact = cont.point;
                Vector2 dir = ((Vector2)transform.position - contact).normalized;

                if (other.gameObject.GetComponent<PropiedadesMat>() != null)
                {
                    rb.AddForce(dir * other.gameObject.GetComponent<PropiedadesMat>().indiceRebote * fRebote, ForceMode2D.Impulse);
                }
                else if (rebotarSiempre)
                    rb.AddForce(dir * fRebote, ForceMode2D.Impulse);
            }
        }
        else
        {
            Vector2 dir = ((Vector2)transform.position - other.contacts[0].point).normalized;
            if (other.gameObject.GetComponent<PropiedadesMat>() != null)
            {
                rb.AddForce(dir * other.gameObject.GetComponent<PropiedadesMat>().indiceRebote * fRebote, ForceMode2D.Impulse);
            }
            else if (rebotarSiempre)
                rb.AddForce(dir * fRebote, ForceMode2D.Impulse);
        }

        if (anim != null && animar) anim.SetTrigger("Rebote");
        rebote = true;
       // ShakeControl.instance.ActivarShake(ShakeControl.FuerzaShake.Debil);


        PropiedadesMat ma = other.gameObject.GetComponent<PropiedadesMat>();
        particulasImpacto.transform.position = other.contacts[0].point;
        if (ma != null) particulasImpacto.CambiarColorInicial(ma.c);
        particulasImpacto.CrearBurst(cantParticulas);
        
    }

    public void LateUpdate()
    {
        rebote = false;
    }


    public void Dead()
    {
        if (muerto != null)
        {
            muerto();
            if (anim != null && animar) anim.SetTrigger("Muerto");
        }
    }

    public void CambiarColor()
    {

    }

    public void MuerteExplosiva()
    {
        if (dead) return;
        if (anim != null)
        {
            anim.SetTrigger("Muerte Explosiva");
            anim.SetBool("Vivo", false);
            //rb.gravityScale = 0;
        }
        if (particulasMuerte != null)
        {
            particulasMuerte.CambiarColorInicial(Recursos.instance.setColorActual.color[2].tono);
            particulasMuerte.transform.position = transform.position;
            particulasMuerte.CrearBurst(50);
        }

        if (audios != null)
        {
            audios.ReproducirAudio(0, 0);
            audios.ReproducirAudio(1, 1);
        }

        rb.simulated = false;
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        dead = true;
        trail.SetActive(false);
        StartCoroutine(Espera(1));
    }

    IEnumerator Espera(float t)
    {
        yield return new WaitForSeconds(t);
        Dead();
    }


}
