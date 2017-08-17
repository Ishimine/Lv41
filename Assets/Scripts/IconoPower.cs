using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class IconoPower : MonoBehaviour
{
    public SpriteRenderer render;

    float vel = 0.5f;
    public Vector3 vels;

    // Use this for initialization
    void Awake ()
    {
        render = GetComponent<SpriteRenderer>();
	}
	
	public void CambiarIcono(Sprite n)
    {
        render.sprite = n;
    }

    public void AnimarIcono()
    {
        StartCoroutine(AnimacionIcono());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator AnimacionIcono()
    {
        render.gameObject.transform.localScale = new Vector3(3,3,2);       
        Color vacio = new Vector4(0, 0, 0, 0);
        render.color = Color.white;
        while (render.color != vacio)
        {
            render.gameObject.transform.localScale = Vector3.Lerp(render.gameObject.transform.localScale, Vector3.zero, vel*Time.fixedDeltaTime);
            render.color = Vector4.Lerp(render.color, vacio, vel * Time.fixedDeltaTime);
            yield return null;
        }
    }
    
}
