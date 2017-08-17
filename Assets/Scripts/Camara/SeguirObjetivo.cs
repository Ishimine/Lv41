using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SeguirObjetivo : MonoBehaviour {

    

    public Transform target;
    public Vector2 correccion;


    public float maxVel;
    public float smooth;

    public Vector2 adelantamientoMax = new Vector2(1,1);


    /// <summary>
    /// Si es true, la camara se adelantara al obejtivo en relacion a su velocidad;
    /// </summary>
    public bool adelantarse;

    private Vector2 actVel;

    static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            SceneManager.sceneLoaded += NivelCargado;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void NivelCargado(Scene scene, LoadSceneMode mode)
    {
        Camera[] a = FindObjectsOfType<Camera>();

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].gameObject != instance)
            {
                Destroy(a[i]);
            }
        }
        
    }






    void Update ()
    {
        if (target == null) return;
        if (adelantarse && target.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 adelantamiento = target.GetComponent<Rigidbody2D>().velocity;
            checkMaxAdelantamiento(ref adelantamiento);
            transform.position = Vector2.SmoothDamp(transform.position, (Vector2)target.position + adelantamiento, ref actVel, smooth, maxVel, Time.deltaTime);   
        }
        else
        {
            transform.position = Vector2.SmoothDamp(transform.position, (Vector2)target.position, ref actVel, smooth, maxVel, Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }


    public void PosicionarCamara(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, -10);
    }

    void checkMaxAdelantamiento(ref Vector2 extra)
    {
        if (Mathf.Abs (extra.x) > adelantamientoMax.x) extra.x = Mathf.Sign(extra.x)*adelantamientoMax.x;
        if (Mathf.Abs(extra.y) > adelantamientoMax.y) extra.y = Mathf.Sign(extra.y)*adelantamientoMax.y;
    }
}
