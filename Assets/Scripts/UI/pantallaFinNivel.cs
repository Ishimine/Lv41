using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pantallaFinNivel : MonoBehaviour {

    public ControlParticula p;


   [SerializeField]  DataDeNivel recordActual;
    [SerializeField] DataDeNivel objetivoActual;

    public Text tiempo;
    public Text muertes;
    public Text barras;
    public Text tiempoRec;
    public Text muertesRec;
    public Text barrasRec;

    public Image iconoMuerte;
    public Image iconoBarras;
    public Image iconoTiempo;

    int m;
    int b;
    float t;

    static pantallaFinNivel instance;
    public Contador contadorBarras;
    public Contador contadorMuertes;
    public Contador contadorTiempo;



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameController.PostFinSesionDeJuego += Activar;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    




    public void Activar()
    {        
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        ActualizarContadores();

    }

    public void ActualizarContadores()
    {
        t = GameController.getTiempo();
        //  tiempo.text = t.ToString("00:00.00");
        tiempo.text = "0.00";        
        b = TouchControl.getBarrasCreadas();
        //  barras.text = b.ToString();
        barras.text = "0";
        m = CheckpointManager.getMuertes();
        //  muertes.text = m.ToString();
        muertes.text = "0";


        ActualizarObjetivo();                           //Extrae los objetivos del nivel
        ExtraerRecord();                             //Actualiza los records del nivel
        ActivarContadores();
        GuardarRecord();

        int x = SceneManager.GetActiveScene().buildIndex;
        ArbitroNiveles.DesbloquearNivel(x);
        ArbitroNiveles.DesbloquearNivel(++x);
        ArbitroNiveles.DesbloquearNivel(++x);

        ActualizarEstadoDeNivel();
    }

    void ActualizarEstadoDeNivel()
    {
        if(recordActual.barras <= objetivoActual.barras &&
           recordActual.tiempo <= objetivoActual.tiempo &&
           recordActual.muertes <= objetivoActual.muertes)
        {
            ArbitroNiveles.SetEstadoNivel(SceneManager.GetActiveScene().buildIndex-1, 1);
        }
    }


    /// <summary>
    /// Aplica records antiguos en los contadores y los activa las animaciones
    /// </summary>
    void ActivarContadores()
    {
        contadorBarras.Iniciar(b, objetivoActual.barras, recordActual.barras);
        contadorMuertes.Iniciar(m, objetivoActual.muertes, recordActual.muertes);
        contadorTiempo.Iniciar(t, objetivoActual.tiempo, recordActual.tiempo);
    }

    IEnumerator RutinaComparar()
    {
        yield return new WaitForSeconds(0.1f);
        CompararObjetivos();
    }
 

    void CompararObjetivos()
    {
        if (ObjetivoCumplido(recordActual.barras, objetivoActual.barras))
        {
            barras.text = recordActual.barras.ToString();
        }
        if (ObjetivoCumplido(recordActual.tiempo, objetivoActual.tiempo))
        {
            tiempo.text = recordActual.tiempo.ToString("00:00.00");
        }
        if (ObjetivoCumplido(recordActual.muertes, objetivoActual.muertes))
        {
            muertes.text = recordActual.muertes.ToString();
        }
    }


    


    public void ActualizarObjetivo()
    {
        objetivoActual = ArbitroNiveles.instance.getDataNivelObjetivos(SceneManager.GetActiveScene().buildIndex);

        /*tiempoRec.text = "/ " + objetivoActual.tiempo.ToString("00:00.00");
        muertesRec.text = "/ " + objetivoActual.muertes.ToString();
        barrasRec.text = "/ " + objetivoActual.barras.ToString();*/
    }

    public void ExtraerRecord()
    {
        int nivelAct = SceneManager.GetActiveScene().buildIndex;
        if (nivelAct > 0)
        {
            recordActual = ArbitroNiveles.instance.getDataNivelRecords(nivelAct);
        }
    }

    public void GuardarRecord()
    {
        int nivelAct = SceneManager.GetActiveScene().buildIndex;

        ArbitroNiveles.ActualizarRecordNivel(nivelAct, new DataDeNivel(nivelAct, b, m, t));
    }


    public bool ObjetivoCumplido(int jugador, int objetivo)
    {
        return objetivo >= jugador;
    }

    public bool ObjetivoCumplido(float jugador, float objetivo)
    {
        return objetivo >= jugador;
    }

    

    IEnumerator AnimarIcono(Text t,Image i)
    {
        yield return new WaitForSeconds(1f);
        float tiempo = 0.1f;
        while (i.color != Color.yellow)
        {
            t.color = Vector4.Lerp(t.color, Color.yellow, tiempo);
            i.color = t.color;
            yield return null;
        }
    }
}
