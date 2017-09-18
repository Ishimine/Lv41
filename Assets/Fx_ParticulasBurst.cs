using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_ParticulasBurst : MonoBehaviour {

    public ParticleSystem p;
    public int cantParticulas = 100;
    public Color cOriginal;





    public void Burst(Vector3 point)
    {
        CambiarColorParticulas(cOriginal);
        BusrtFinal(point);
    }

    void BusrtFinal(Vector3 point)
    {
        p.transform.position = point;
        p.Play();
        StartCoroutine(Apagar());
    }

    void CambiarColorParticulas(Color c)
    {
        var main = p.main;
        main.startColor = c;
    }
    public void Burst(Vector3 point,Color c)
    {
        CambiarColorParticulas(c);
        BusrtFinal(point);
            }

    IEnumerator Apagar()
    {
        yield return new WaitForSeconds(.1f);
        p.Stop();
    }
}
