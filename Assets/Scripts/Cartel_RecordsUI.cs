using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Cartel_RecordsUI : MonoBehaviour {

    public Animator anim;

    public Text txtBarras;
    public Text txtTiempo;
    public Text txtMuertes;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameController.PreSesionDeJuego += Activar;
    }

    public void Activar()
    {
        anim.SetTrigger("Activar");
        ActualizarRecords();
    }

    void ActualizarRecords()
    {
       DataDeNivel records = ArbitroNiveles.instance.getDataNivelObjetivos(SceneManager.GetActiveScene().buildIndex);
        txtBarras.text = records.barras.ToString();
        txtMuertes.text = records.muertes.ToString();
        txtTiempo.text = records.tiempo.ToString("0:00.00");
    }

    private void OnDestroy()
    {
        GameController.PreSesionDeJuego -= Activar;
    }
}
