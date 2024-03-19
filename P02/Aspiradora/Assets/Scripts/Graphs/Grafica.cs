using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafica{

    public List<Vertice> grafica = new List<Vertice>();
	public List<Vertice> camino = new List<Vertice>();

	//Agrega un vértice a la lista de vértices de la gráfica.
    public void AgregarVertice(Vertice nuevoVertice) {
        //Completar
    }

	//Aplica el Algoritmo de A*
	public bool AStar(Vertice inicio, Vertice final) {
		//Completar
		return true;
    }

	//Auxiliar que reconstruye el camino de A*
	public void reconstruirCamino(Vertice inicio, Vertice final) {
		//Completar
	}

	float distancia(Vertice a, Vertice b) {
		//Completar
		return 0;
	}

	int menorF(List<Vertice> l) {
		//Coompletar
		return 0;
	}

	//Método que da una representación escrita de la gráfica.
	public string toString() {
		string aux = "\nG:\n";
		foreach (Vertice v in grafica) {
			aux += v.toString() + "\n";
		}
		return aux;
	}

}
