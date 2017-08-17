using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Impacto : MonoBehaviour {


    public bool destellar;
    public bool usarAnimator = false;
    public bool cambiarEscala = false;
    public Animator anim;

    public bool UsarHijos = false;


    public float aumentoEscala = 1.1f;
    public Color cDestello = Color.white;
    public Color original;
    public Color cActual;
    Color c;
    public float velT = 0.2f;
    float t = 0;

    public SpriteRenderer render;

    public Vector3 escalaOriginal;
    public Vector3 escalaMax;



    void Start () {
        if(render == null) render = GetComponent<SpriteRenderer>();
        original = render.color;
    }

    private void Awake()
    {
        escalaOriginal = render.transform.localScale;
        escalaMax = render.transform.localScale * aumentoEscala;
    }



    public void Activar()
    {
        cActual = cDestello;
        t = 0;
        StartCoroutine(DestelloFX());
        if (anim != null && usarAnimator)
        {
            anim.SetTrigger("Activado");
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" || this.tag == "Player")
        {
            Activar();
        }
    }



    IEnumerator DestelloFX()
    {
        if (cambiarEscala) render.transform.localScale = escalaMax;
        do
        {
            cActual = Vector4.Lerp(cDestello, original, t);
            if (cambiarEscala) render.transform.localScale = Vector4.Lerp(render.transform.localScale, escalaOriginal, t);
            if (destellar) render.color = cActual;
            t += velT * Time.deltaTime;
            yield return null;
        } while (t < 1);

        if (destellar) render.color = original;
        if (cambiarEscala) render.transform.localScale = escalaOriginal;

        StopCoroutine(DestelloFX());
    }



    /// <summary>
    /// Vuelve el objeto transparente y desactica totalmente la interaccion fisica
    /// </summary>
    public void Desactivar()
    {

    }
}
