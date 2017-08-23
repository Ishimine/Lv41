using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaJugador : MonoBehaviour {

    /// <summary>
    /// Si es False, solo rebotara contra objetos que tengan el componente PropiedadesMat. Caso contrario con objetos sin Propiedades Mat
    /// rebotara igual al fRebote
    /// </summary>
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
    }

    void InicializarJugador()
    {
        if (anim != null) anim.SetTrigger("Vivo");
        //Aplicar Skin
        //ReiniciarVida
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
        ShakeControl.instance.ActivarShake(ShakeControl.FuerzaShake.Debil);
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
        if (anim != null)
        {
            anim.SetTrigger("Muerte Explosiva");
            //rb.gravityScale = 0;
        }

        StartCoroutine(Espera(1));
    }

    IEnumerator Espera(float t)
    {
        yield return new WaitForSeconds(t);
        Dead();
    }


}
