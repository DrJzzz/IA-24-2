using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafica{

    public List<Vertice> grafica = new List<Vertice>();
	public List<Vertice> camino = new List<Vertice>();

	//Agrega un v�rtice a la lista de v�rtices de la gr�fica.
    public void AgregarVertice(Vertice nuevoVertice) {
        this.grafica.Add(nuevoVertice);
    }

	//Aplica el Algoritmo de A*
	public bool AStar(Vertice inicio, Vertice final)
	{
		List<Vertice> openSet = new List<Vertice>();
		HashSet<Vertice> closedSet = new HashSet<Vertice>();
		openSet.Add(inicio);
		while (openSet.Count > 0) {
			Vertice currentNodo = openSet[0];
			for (int i = 1; i < openSet.Count; i++) {
				if (openSet[i].fCost < currentNodo.fCost || 
				    openSet[i].fCost == currentNodo.fCost && openSet[i].hCost < currentNodo.hCost )
				{
					currentNodo = openSet[i];
				}

				openSet.Remove(currentNodo);
				closedSet.Add(currentNodo);
				if (currentNodo == final) {
					reconstruirCamino(inicio, final);
				}

				foreach (Vertice vecino in currentNodo.vecinos) {
					if (closedSet.Contains(vecino)) {
						continue;
					}

					float costoDeVecino = currentNodo.gCost + distancia(currentNodo, vecino);

					if (costoDeVecino > vecino.gCost || !openSet.Contains(vecino))
					{
						vecino.gCost = costoDeVecino;
						vecino.hCost = distancia(vecino, final);
						vecino.setPadre(currentNodo);
						if (!openSet.Contains(vecino))
						{
							openSet.Add(vecino);
						}
					}
				}
			}
		}
		return true;
    }

	//Auxiliar que reconstruye el camino de A*
	public void reconstruirCamino(Vertice inicio, Vertice final)
	{
		Vertice verticeActual = final;
		while (verticeActual != inicio)
		{
			this.camino.Add(verticeActual);
			verticeActual = verticeActual.padre;
		}

		this.camino.Reverse();
	}

	float distancia(Vertice a, Vertice b)
	{
		return Vector3.Distance(a.posicion, b.posicion);
	}

	float menorF(List<Vertice> l)
	{

		float menorFCost = l[0].fCost;
		foreach (Vertice vecino in l)
		{
			if (vecino.fCost < menorFCost)
			{
				menorFCost = vecino.fCost;
			}
		}

		return menorFCost;
	}

	//M�todo que da una representaci�n escrita de la gr�fica.
	public string toString() {
		string aux = "\nG:\n";
		foreach (Vertice v in grafica) {
			aux += v.toString() + "\n";
		}
		return aux;
	}

}
