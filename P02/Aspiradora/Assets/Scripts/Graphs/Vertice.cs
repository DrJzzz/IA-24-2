using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertice{

    public List<Vertice> vecinos = new List<Vertice>();
    public int id;
    public Vertice padre;
    public Vertice camino;
    public Vector3 posicion;
    public float gCost;
    public float hCost;
    

    public Vertice(int newId, Vector3 newPos)
    {
        this.id = newId;
        this.posicion = newPos;
        
    }

    public void setPadre(Vertice padre)
    {
        this.padre = padre;
    }

    public void AgregarVecino(Vertice newVertice) {
        this.vecinos.Add(newVertice);
    }

    public float fCost {
        get
        {
            return gCost + hCost;
        }
    }

    //M�todo que convierte a cadena el v�rtice, contiene su ID, ID de vecinos y padre.
    public string toString() {
        string aux = id.ToString()+":";
        foreach(Vertice v in vecinos) {
            aux += v.id + ",";
        }
        if (padre != null) {
            aux += "P:" + padre.id;
        }
        return aux;
    }
}
