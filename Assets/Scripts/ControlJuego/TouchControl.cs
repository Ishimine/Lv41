using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchControl : MonoBehaviour
{
    public float fuerzaMax = 5;
    public float fuerzaMin = 1;


    public bool mostrarTouchs;
    public SpriteRenderer touchRender;
    public SpriteRenderer dobleTapRender;

    public enum barrasSys { Clásico, PorTiempo, PorTap };
    public barrasSys barrasVida;

    public enum TipoBarras { Normales, NinjaUp, NinjaUpInv };
    public TipoBarras tipoBarras;

    public enum TipoCreacion { Normal, Estatico };
    public TipoCreacion tipoCreacion;

    public float grosorBarra = 1;

    public bool permitirCrearMultiplesBarras = false;

    public bool usarMouse = false;


    public delegate void Trigger();
    public Trigger dobleTap;

    [SerializeField] private bool dobleTapTrigger = false;


    private struct idTouch
    {
        public int fingerId;
        public int barraId;
        public Vector2 posInicial;
        public Vector2 touchInicial;

        public idTouch(int fingerId, int barraId, Vector2 posInicial, Vector2 tInicial)
        {
            this.fingerId = fingerId;
            this.barraId = barraId;
            this.posInicial = posInicial;
            this.touchInicial = tInicial;
        }
    }



    static int barrasCreadas = 0;
    public delegate void Act(int i);
    public Act bCreada;


    public static TouchControl instance;
    public int touchCantMax = 3;

    /// <summary>
    /// Tiempo de vida de una plataforma/barra en el sistem de vida de barra "PorTiempo"
    /// </summary>
    public float tVidaBarra = 1f;



    Vector2[] touchPosInicial = new Vector2[3];

    Vector2 posInicial;

    public GameObject prefabBarraFisica;
    private GameObject[] barrasF = new GameObject[3];
    public GameObject prefabBarraTransparente;
    private GameObject[] barrasT = new GameObject[3];

    List<idTouch> listaT = new List<idTouch>();

    public float tamañoBarra = 0.5f;
    public float longBarraMax = 100;
    public float longBarraMin = 0.25f;

    [SerializeField] int bActualT = 0;
    [SerializeField] int bActualF = 0;

    //MOUSE

    //Vector2 posInicial;


    public static int getBarrasCreadas()
    {
        return barrasCreadas;
    }

    public static void Reiniciar()
    {
        BorrarBarras();
    }


    void Awake()
    {
        if (instance != null)
        {

            Destroy(this.gameObject);
        }
        else
        {
            if (Application.isMobilePlatform) usarMouse = false;
            else if (Application.isEditor) usarMouse = true;


            instance = this;
            SelectorNivel.NivelCargado += NivelCargado;
            InicializarBarras();
        }
    }

    void NivelCargado()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            OcultarBarras();
            listaT.Clear();
            barrasCreadas = 0;
            GameController.instance.player.GetComponent<EsferaJugador>().muerto += OcultarBarras;
        }
    }

    void NivelCargado(Scene scene, LoadSceneMode mode)
    {
        NivelCargado();
    }

    public static void BorrarBarras()
    {
        barrasCreadas = 0;
        if (instance.bCreada != null) instance.bCreada(barrasCreadas);
        instance.listaT.Clear();
        //instance.OcultarBarras();

        GameObject[] barras = GameObject.FindGameObjectsWithTag("BarraJugador");

        foreach(GameObject g in barras)
        {
            Destroy(g);
        }
    }

    public void OcultarBarras()
    {
        for (int i = 0; i < barrasF.Length; i++)
        {
            barrasF[i].SetActive(false);
            barrasT[i].SetActive(false);
        }
    }

    private void InicializarBarras()
    {
        if (barrasVida == barrasSys.PorTiempo)
        {
            barrasF = new GameObject[10];
            barrasT = new GameObject[10];
        }
        else
        {
            barrasF = new GameObject[3];
            barrasT = new GameObject[3];
        }
        for (int i = 0; i < barrasF.Length; i++)
        {
            barrasF[i] = Instantiate(prefabBarraFisica);
            barrasF[i].SetActive(false);
            DontDestroyOnLoad(barrasF[i]);

            // barrasF[i].transform.localScale = new Vector2(tamañoBarra, tamañoBarra);

            barrasT[i] = Instantiate(prefabBarraTransparente);
            barrasT[i].SetActive(false);
            DontDestroyOnLoad(barrasT[i]);
            // barrasT[i].transform.localScale = new Vector2(tamañoBarra, tamañoBarra);

        }
    }

    void Update()
    {
        if (GameController.enMenu || GameController.enPausa || !GameController.swipeActivo )
            return;

#if UNITY_ANDROID
        if (permitirCrearMultiplesBarras) CrearBarrasTouch();
        else CrearBarrasTouchSimple();




        if (barrasVida == barrasSys.PorTap)
        {
            TapResetBarras();
        }
        else
        {
            PowerTap();
        }
#endif
        
      if (usarMouse)  CrearBarrasMouse();

    }


    void TapResetBarras()
    {
        foreach (Touch t in Input.touches)
        {
            if(t.tapCount == 2)
            {
                OcultarBarras();
            }
        }
    }

    void PowerTap()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.tapCount == 2)
            {
                if (!dobleTapTrigger)
                {
                    if (dobleTap != null)
                        dobleTap();
                    dobleTapTrigger = true;
                }
                else if(t.phase == TouchPhase.Ended)
                {
                    dobleTapTrigger = false;
                }

                if(mostrarTouchs)
                {
                    DobleTapVisual(t.position);
                }
            }
        }
    }


    void CrearBarrasTouchSimple()
    {
        if(Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                touchIn(0, t.position);
            }
            else if (t.phase == TouchPhase.Moved)
            {
                touchMove(0, t.position);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                touchEnd(0, t.position);
                //barrasCreadas++;
            }
        }
    }


    void CrearBarrasTouch()
    {       

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                touchIn(t.fingerId, t.position);
                /*if (mostrarTouchs)
                {
                   TouchVisual(t.position);
                }*/
            }
            else if (t.phase == TouchPhase.Moved)
            {
                touchMove(t.fingerId, t.position);
                if (mostrarTouchs)
                {
                   TouchVisual(t.position);
                   // touchRender.transform.position = t.position;
                }

            }
            else if (t.phase == TouchPhase.Ended)
            {
                touchEnd(t.fingerId, t.position);               
            }
        }
    }

    void CrearBarrasMouse()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            touchIn(0, Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            touchMove(0, Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchEnd(0, Input.mousePosition);
            //barrasCreadas++;
        }
    }





    /// <summary>
    /// Posiciona la barra transparente en la posicion inicial del touch
    /// </summary>
    /// <param name="id"></param>
    private void touchIn(int fingerId, Vector2 actPos)
    {
        //Se guarda el fingerID en una lista + Posicion inicial + barra asignada
        idTouch t ;
        t = new idTouch(fingerId, proxBarraT(), Camera.main.ScreenToWorldPoint(actPos) - new Vector3(0f, 0f, Camera.main.transform.position.z), actPos);
        

        listaT.Add(t);


        //Se posiciona barraT en la pos indicada con una escala de 0 en eje x;
        //print(t.barraId);
        barrasT[t.barraId].transform.position = t.posInicial;
        barrasT[t.barraId].transform.localScale = new Vector3(0,1,1);
        barrasT[t.barraId].SetActive(true);
    }
    

    /// <summary>
    /// Actualiza la longitud y rotacion de la barra transparente en relacion al arrastre del touch
    /// </summary>
    /// <param name="id"></param>
    private void touchMove(int fingerId, Vector2 actPos)
    {

        //Obtener t de la lista
        idTouch t = listaT.Find(x => x.fingerId == fingerId);

        //Calculamos direccion y longitud de la barra
        Vector2 target = Camera.main.ScreenToWorldPoint(actPos);         //Punto del touch actual
        float dist;


        Vector2 dif;
        float angle;

        if (tipoCreacion == TipoCreacion.Normal)
        {
            dist = Vector2.Distance(t.posInicial, target) / 4;                                        //Distancia entre actual e inicial
            dif = target - t.posInicial;                                                        //Dif de la rotacion

        }
        else
        {
            dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(t.touchInicial), target) / 4;
            dif = (Vector3)target - Camera.main.ScreenToWorldPoint(t.touchInicial) + Vector3.forward*40;
        }

        if (dist > longBarraMax) dist = longBarraMax;                                              //Si el tamaño supera el maximo se limita al mismo



        angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;                                                    //Angulo de la rotacion



        barrasT[t.barraId].transform.localScale = new Vector3(dist, grosorBarra, 1);                            //Aplicamos longitud de la barraT

        barrasT[t.barraId].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);                     //Aplicamos rotacion de la barraT


        if (tipoCreacion == TipoCreacion.Estatico)
        {
            // barrasT[t.barraId].transform.position += Camera.main.transform.position + Vector3.forward*10 - barrasT[t.barraId].transform.position;
            barrasT[t.barraId].transform.position = Camera.main.ScreenToWorldPoint(t.touchInicial) + Vector3.forward * 40;
        }

    }


    private void touchEnd(int fingerId, Vector2 actPos)
    {
        if (listaT.Count == 0)
            return;
        //Obtener t de la lista
        idTouch t = listaT.Find(x => x.fingerId == fingerId);
        listaT.Remove(t);       //eliminamos de la lista

        //Calculamos direccion y longitud de la barra
        Vector2 target = Camera.main.ScreenToWorldPoint(actPos);         //Punto del touch actual


        barrasT[t.barraId].SetActive(false);


        //print("PosInicial: " + t.posInicial);

        //print("PosAct: " + target);

        //calculamos distancia y si es menor al minimo cancelamos todo
        float dist = Vector2.Distance(t.posInicial, target) / 4;
        if(dist > longBarraMin)
        {
            if (dist > longBarraMax) dist = longBarraMax;
            Vector2 dif = target - t.posInicial;
            if (barrasVida == barrasSys.PorTiempo)
            {
                GameObject clone = Instantiate<GameObject>(prefabBarraFisica);
                EndTouch_PosicionarBarra(clone, dist, target, t);
                Destroy(clone, tVidaBarra);

                if(tipoBarras != TipoBarras.Normales)
                {
                    bool m = true;
                    if (tipoBarras == TipoBarras.NinjaUp) m = false;

                    clone.GetComponentInChildren<PropiedadesMat>().indiceRebote = CalcularIndiceRebote(dist, m);
                }

            }
            else
            {                
                EndTouch_PosicionarBarra(barrasF[actBarraF()], dist, target, t);
                proxBarraF();
            }            
        }
        else
        {
            antBarraF();
        }
    }

    float CalcularIndiceRebote(float dist,bool m)
    {
        float a = dist/ longBarraMax;
        //print("dist" + a);
        if(m) return Mathf.Lerp(fuerzaMin, fuerzaMax, a);
        else return Mathf.Lerp(fuerzaMax, fuerzaMin, a);
    }


    void EndTouch_PosicionarBarra(GameObject obj, float dist, Vector2 target, idTouch t)
    {
        if (dist > longBarraMax) dist = longBarraMax;

        Vector2 dif;
        if (tipoCreacion == TipoCreacion.Normal)
            dif = target - t.posInicial;                                                        //Dif de la rotacion
        else
            dif = target - (Vector2)Camera.main.ScreenToWorldPoint(t.touchInicial);
        float angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

        if (tipoCreacion == TipoCreacion.Normal) obj.transform.position = t.posInicial;
        else obj.transform.position = Camera.main.ScreenToWorldPoint(t.touchInicial) + Vector3.forward*10;


        obj.transform.localScale = new Vector3(dist, grosorBarra, 1);                          //Aplicamos longitud de la barraT
        obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        obj.SetActive(true);
        barrasCreadas++;
        if (bCreada != null) bCreada(barrasCreadas);

    }



    private int proxBarraT()
    {
        bActualT++;
        if (bActualT > 2) bActualT = 0;
        return bActualT;
    }
    private int actBarraT()
    {
        return bActualT;
    }

    private int antBarraT()
    {
        bActualT--;
        if (bActualT < 0)
            bActualT = 2;
        return bActualT;
    }
    private int proxBarraF()
    {
        ++bActualF;
        if (bActualF > 2) bActualF = 0;
        return bActualF;
    }
    private int actBarraF()
    {
        return bActualF;
    }


    private int antBarraF()
    {
        bActualF--;
        if (bActualF < 0)
            bActualF = 2;
        return bActualF;
    }



    public void SetTipoBarra(int x)
    {
        tipoBarras = (TipoBarras)x;
    }


    public void SetTipoCreacion(int x)
    {
        tipoCreacion = (TipoCreacion)x;
    }





    #region Visualizador De touchs

    public void TouchVisual(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos) - new Vector3(0f, 0f, Camera.main.transform.position.z);
        touchRender.gameObject.transform.position = pos;
        StartCoroutine(AnimacionIcono(touchRender, 0.15f ));
    }

    public void DobleTapVisual(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos) - new Vector3(0f, 0f, Camera.main.transform.position.z);

        dobleTapRender.gameObject.transform.position = pos;
        StartCoroutine(AnimacionIconoDobleTap(dobleTapRender,0.45f));
    }

    IEnumerator AnimacionIcono(SpriteRenderer render, float vel)
    {
        render.gameObject.SetActive(true);
        render.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 2);
        Color vacio = new Vector4(0, 0, 0, 0);
        render.color = Color.white;
        while (render.color != vacio)
        {
            render.gameObject.transform.localScale = Vector3.Lerp(render.gameObject.transform.localScale, Vector3.zero, vel * Time.deltaTime);
            render.color = Vector4.Lerp(render.color, vacio, vel * Time.deltaTime);
            yield return null;
        }
        render.gameObject.SetActive(false);

    }


    IEnumerator AnimacionIconoDobleTap(SpriteRenderer render, float vel)
    {
        render.gameObject.transform.localScale = new Vector3(1,1,2);
        Color vacio = new Vector4(0, 0, 0, 0);
        render.color = Color.white;
        while (render.color != vacio)
        {
            render.gameObject.transform.localScale = Vector3.Lerp(render.gameObject.transform.localScale, Vector3.one*3, vel * Time.deltaTime);
            render.color = Vector4.Lerp(render.color, vacio, vel * Time.deltaTime);
            yield return null;
        }
    }

    #endregion
}