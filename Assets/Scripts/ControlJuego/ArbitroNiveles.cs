using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ArbitroNiveles : MonoBehaviour {

    

    public static ArbitroNiveles instance;




    public DataDeNivel[] recordsAux;
    public DataDeNivel[] objetivosAux;

    //public static DataDeNivel[] records;
    //public static DataDeNivel[] objetivos;




    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //CargarObjetivos();
            CargarRecords();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    


    public DataDeNivel getDataNivelObjetivos(int i)
    {
        return objetivosAux[i-1];
    }
        

    public DataDeNivel getDataNivelRecords(int i)
    { 
        return recordsAux[i-1];
    }

    public static void ActualizarRecordNivel(int i, DataDeNivel puntajeActual)
    {
        i --;
        if (instance.recordsAux[i].barras > puntajeActual.barras)
            instance.recordsAux[i].barras = puntajeActual.barras;
        if (instance.recordsAux[i].muertes > puntajeActual.muertes)
            instance.recordsAux[i].muertes = puntajeActual.muertes;
        if (instance.recordsAux[i].tiempo > puntajeActual.tiempo)
            instance.recordsAux[i].tiempo = puntajeActual.tiempo;

        //instance.ActRecords();
        instance.GuardarRecords();

    }


    public void CargarObjetivos()
    {
        CargarArreglo(ref objetivosAux, "objetivos");
    }

    public void CargarRecords()
    {
        CargarArreglo(ref recordsAux, "records");
    }    
    

    public void GuardarObjetivos()
    {
        GuardarArreglo(ref objetivosAux, "objetivos");
    }

    public void GuardarRecords()
    {
        GuardarArreglo(ref recordsAux, "records");
    }

    public static void CargarArreglo(ref DataDeNivel[] ar, string nombreFisico)
    {
        if (File.Exists(Application.persistentDataPath + "/" + nombreFisico))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + nombreFisico, FileMode.Open);
            ar = (DataDeNivel[])bf.Deserialize(file);
            file.Close();
        }
        /*else if(nombreFisico == "records")
        {
            InicializarRecords();   
        }
        else if(nombreFisico == "objetivos")
        {

        }*/
    }

    public static void GuardarArreglo(ref DataDeNivel[] ar, string nombreFisico)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + nombreFisico);
        bf.Serialize(file, ar);
        file.Close();
    }


    /* public void OnValidate()
     {
         ActRecords();
         ActObjetivos();
     }*/

    /*
private void ActRecords()
{
    records = recordsAux;
}

private void ActObjetivos()
{
    objetivos = objetivosAux;
}*/
    public void DesbloquearNiveles()
    {
        for (int i = 0; i < recordsAux.Length; i++)
        {
            recordsAux[i].idNivel = 0;
        }
        GuardarRecords();
    }

    public static int GetEstadoNivel(int x)
    {
       return instance.recordsAux[x].idNivel;
    }

    public static void DesbloquearNivel(int x)
    {
        if (instance.recordsAux[x].idNivel == 1) return;
        else        instance.recordsAux[x].idNivel = 0;
    }

    public static void SetEstadoNivel(int id, int estado)
    {
        instance.recordsAux[id].idNivel = estado;
        instance.GuardarRecords();
    }

    public void ResetRecords()
    {
        for(int i = 0; i < recordsAux.Length; i++ )
        {
            recordsAux[i].idNivel = -1;
            recordsAux[i].barras = 999;
            recordsAux[i].muertes = 999;
            recordsAux[i].tiempo = 999;
        }
        recordsAux[0].idNivel = 0;
        GuardarRecords();
    }
}
