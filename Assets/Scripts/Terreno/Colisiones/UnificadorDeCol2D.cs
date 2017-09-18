﻿using UnityEngine;
using System.Collections.Generic;
using System;

using ClipperLib;
using Path = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Paths = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;



public class UnificadorDeCol2D : MonoBehaviour
{

    public PolyFillType fillSub;
    public PolyFillType fillClip;


    public float grosor = 10f;
    public GameObject cuerpo;
    public PropiedadesMat mat;

    public void AgregarPolygonHijos()
    {
        int a = transform.childCount;
        for (int i = 0; i < a; i++)
        {
            PolygonCollider2D col = transform.GetChild(i).GetComponent<PolygonCollider2D>();
            if (col == null)
            {
                col.gameObject.AddComponent<PolygonCollider2D>();
            }
            
        }
    }
    public void EliminarPolygonHijos()
    {
        int a = transform.childCount;
        for (int i = 0; i < a; i++)
        {
            PolygonCollider2D col = transform.GetChild(i).GetComponent<PolygonCollider2D>();
            if (col != null)
            {
                PropiedadesMat mat = col.GetComponent<PropiedadesMat>();
                DestroyImmediate(mat);
                DestroyImmediate(col);
            }
        }
    }



    public void DeshabilitarPolygonHijos()
    {
        int a = transform.childCount;
        for (int i = 0; i < a; i++)
        {
            PolygonCollider2D col = transform.GetChild(i).GetComponent<PolygonCollider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }

    public void ActivarPolygonHijos()
    {
        int a = transform.childCount;
        for (int i = 0; i < a; i++)
        {
            PolygonCollider2D col = transform.GetChild(i).GetComponent<PolygonCollider2D>();
            if (col != null)
            {
                col.enabled = true;
            }
        }
    }


    public void CrearColliderUnificado()
    {
        mat = null;
        //CreateLevelCollider(UniteCollisionPolygons(ExtraerPoligonosEnHijos()));
        CreateLevelCollider(UniteCollisionPolygons(ExtraerPoligonosEnHijos()));


        //PropiedadesMat prop = GetComponentInChildren<PropiedadesMat>();
        PropiedadesMat prop = transform.GetChild(0).GetComponent<PropiedadesMat>();
        if (prop != null)
        {
            PropiedadesMat propLocal = GetComponent<PropiedadesMat>();
            if (propLocal == null)
                 propLocal = gameObject.AddComponent<PropiedadesMat>();
            propLocal.indiceRebote = prop.indiceRebote;           
            propLocal.c = prop.c;
        }
    }

    public void CrearTerrenoFinal()
    {
        mat = null;
        //CreateLevelCollider(UniteCollisionPolygons(ExtraerPoligonosEnHijos()));
        CreateLevelCollider(UniteCollisionPolygons(ExtraerPoligonosRecursivo()));
    }



    private List<List<Vector2>> ExtraerPoligonosRecursivo()
    {
        List<List<Vector2>> list = new List<List<Vector2>>();
        return ExtraerPoligonosRecursivo(ref list,gameObject);
    }
    
    public List<List<Vector2>> ExtraerPoligonosRecursivo(ref List<List<Vector2>> lista, GameObject obj)
    {
        if (obj.GetComponent<PolygonCollider2D>() != null)
        {
            ExtraerPoligonosSimple(obj.GetComponent<PolygonCollider2D>(),ref lista);
        }
        else
        {
            foreach (Transform child in transform)
            {
                ExtraerPoligonosRecursivo(ref lista, child.gameObject);
            }
        }
        return lista;
    }

    public List<List<Vector2>> ExtraerPoligonosEnHijos()
    {
        List<List<Vector2>> lista = new List<List<Vector2>>();
        foreach (Transform child in transform)
        {
            PolygonCollider2D col = child.GetComponent<PolygonCollider2D>();
            if (col != null)
            {
                ExtraerPoligonosSimple(col, ref lista);
            }
        }
        return lista;
    }


    public List<List<Vector2>> ExtraerPoligonosFinal()
    {
        List<List<Vector2>> lista = new List<List<Vector2>>();
        foreach (Transform child in transform)
        {
            PolygonCollider2D col = child.GetComponent<PolygonCollider2D>();
            if (col != null)
            {
                ExtraerPoligonosSimple(col, ref lista);
            }
        }
                return lista;
    }



    public List<List<Vector2>> ExtraerPoligonosSimple(PolygonCollider2D col,ref List<List<Vector2>> l)
    {
        int t = col.pathCount;
        for(int i = 0; i<t;i++)
        {
            Vector2[] vec = col.GetPath(i);
            List<Vector2> lVec = new List<Vector2>();

            for(int b=0;b<vec.Length;b++)
            {
                vec[b] = new Vector2(vec[b].x * Math.Sign(col.transform.localScale.x), vec[b].y * Math.Sign(col.transform.localScale.y));
                lVec.Add(vec[b]+(Vector2)col.transform.position);                
            }
            l.Add(lVec);
        }



        /*
        int t = col.GetTotalPointCount();
        List<Vector2> l = new List<Vector2>();
        for(int i = 0; i < t; i++)
        {
            l.Add(col.points[i] + (Vector2)col.transform.position);
        }*/
        return l;
    }
    


    //this function takes a list of polygons as a parameter, this list of polygons represent all the polygons that constitute collision in your level.
    public List<List<Vector2>> UniteCollisionPolygons(List<List<Vector2>> polygons)
    {
        //this is going to be the result of the method
        List<List<Vector2>> unitedPolygons = new List<List<Vector2>>();
        Clipper clipper = new Clipper();

        //clipper only works with ints, so if we're working with floats, we need to multiply all our floats by
        //a scaling factor, and when we're done, divide by the same scaling factor again
        int scalingFactor = 10000;

        //this loop will convert our List<List<Vector2>> to what Clipper works with, which is "Path" and "IntPoint"
        //and then add all the Paths to the clipper object so we can process them
        for (int i = 0; i < polygons.Count; i++)
        {
            Path allPolygonsPath = new Path(polygons[i].Count);

            for (int j = 0; j < polygons[i].Count; j++)
            {
                allPolygonsPath.Add(new IntPoint(Mathf.Floor(polygons[i][j].x * scalingFactor), Mathf.Floor(polygons[i][j].y * scalingFactor)));
            }
            clipper.AddPath(allPolygonsPath, PolyType.ptSubject, true);

        }

        //this will be the result
        Paths solution = new Paths();

        //having added all the Paths added to the clipper object, we tell clipper to execute an union
        //clipper.Execute(ClipType.ctUnion, solution);
        clipper.Execute(ClipType.ctUnion, solution, fillSub,fillClip);


        //the union may not end perfectly, so we're gonna do an offset in our polygons, that is, expand them outside a little bit
        ClipperOffset offset = new ClipperOffset();
        offset.AddPaths(solution, JoinType.jtMiter, EndType.etClosedPolygon);
        //5 is the ammount of offset
        offset.Execute(ref solution, grosor);

        //now we just need to conver it into a List<List<Vector2>> while removing the scaling
        foreach (Path path in solution)
        {
            List<Vector2> unitedPolygon = new List<Vector2>();
            foreach (IntPoint point in path)
            {
                unitedPolygon.Add(new Vector2(point.X / (float)scalingFactor, point.Y / (float)scalingFactor));
            }
            unitedPolygons.Add(unitedPolygon);
        }

        //this removes some redundant vertices in the polygons when they are too close from each other
        //may be useful to clean things up a little if your initial collisions don't match perfectly from tile to tile
        unitedPolygons = RemoveClosePointsInPolygons(unitedPolygons);

        //everything done
        return unitedPolygons;
    }

    //create the collider in unity from the list of polygons
    public void CreateLevelCollider(List<List<Vector2>> polygons)
    {
        /*
        cuerpo = new GameObject("Cuerpo");
        //colliderObj.layer = GR.inst.GetLayerID(Layer.PLATFORM);
        cuerpo.transform.SetParent(transform);
        

        PolygonCollider2D collider = cuerpo.AddComponent<PolygonCollider2D>();
        
    
        */



        PolygonCollider2D collider = gameObject.GetComponent<PolygonCollider2D>();
        if(collider == null)
        {
            collider = gameObject.AddComponent<PolygonCollider2D>();
        }

        collider.pathCount = polygons.Count;

        for (int i = 0; i < polygons.Count; i++)
        {
            Vector2[] points = polygons[i].ToArray();

            for(int a = 0; a<points.Length;a++)
            {
                points[a] -= (Vector2)transform.position;
            }//Corrige la posicion del colisionador

            collider.SetPath(i, points);
        }        
    }

    public List<List<Vector2>> RemoveClosePointsInPolygons(List<List<Vector2>> polygons)
    {
        float proximityLimit = 0.1f;

        List<List<Vector2>> resultPolygons = new List<List<Vector2>>();

        foreach (List<Vector2> polygon in polygons)
        {
            List<Vector2> pointsToTest = polygon;
            List<Vector2> pointsToRemove = new List<Vector2>();

            foreach (Vector2 pointToTest in pointsToTest)
            {
                foreach (Vector2 point in polygon)
                {
                    if (point == pointToTest || pointsToRemove.Contains(point)) continue;

                    bool closeInX = Math.Abs(point.x - pointToTest.x) < proximityLimit;
                    bool closeInY = Math.Abs(point.y - pointToTest.y) < proximityLimit;

                    if (closeInX && closeInY)
                    {
                        pointsToRemove.Add(pointToTest);
                        break;
                    }
                }
            }
            polygon.RemoveAll(x => pointsToRemove.Contains(x));

            if (polygon.Count > 0)
            {
                resultPolygons.Add(polygon);
            }
        }

        return resultPolygons;
    }
}
