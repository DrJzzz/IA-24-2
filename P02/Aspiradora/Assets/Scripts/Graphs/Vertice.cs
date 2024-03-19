using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertice{

    public List<Vertice> vecinos = new List<Vertice>();
    public int id;
    public Vertice padre;
    public Vertice camino;
    public Vector3 posicion;

    public Vertice(int newId, Vector3 newPos) {
        //Completar
    }

    public void setPadre(Vertice padre) {
        //Completar
    }

    public void AgregarVecino(Vertice newVertice) {
        //Completar
    }

    //Método que convierte a cadena el vértice, contiene su ID, ID de vecinos y padre.
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
