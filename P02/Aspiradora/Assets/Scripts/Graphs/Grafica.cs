using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grafica{

    public List<Vertice> grafica = new List<Vertice>();
	public List<Vertice> camino = new List<Vertice>();
	public LayerMask obstaculos;
	//Agrega un v�rtice a la lista de v�rtices de la gr�fica.
    public void AgregarVertice(Vertice nuevoVertice) {
        this.grafica.Add(nuevoVertice);
    }

	//Aplica el Algoritmo de A*
	public bool AStar(Vertice inicio, Vertice final)
	{
		
		List<Vertice> sinEvaluar = new List<Vertice>();
		HashSet<Vertice> evaluados = new HashSet<Vertice>();
		sinEvaluar.Add(inicio);
		while (sinEvaluar.Count > 0) {
			Vertice actualVertice = sinEvaluar[0];
			actualVertice = menorF(sinEvaluar, actualVertice);
			sinEvaluar.Remove(actualVertice);
			evaluados.Add(actualVertice);
				if (actualVertice.Equals(final)) {
					reconstruirCamino(inicio, final);
					return true;
				}

				foreach (Vertice vecino in actualVertice.vecinos)
				{
					if (!esCaminable(actualVertice,vecino) || evaluados.Contains(vecino))
					{
						continue;
					}

					float costoDeVecino = actualVertice.gCost + distancia(actualVertice, vecino);
					if (costoDeVecino < vecino.gCost || !sinEvaluar.Contains(vecino))
					{
						
						vecino.gCost = costoDeVecino;
						vecino.hCost = distancia(vecino, final);
						vecino.setPadre(actualVertice);
						if (!sinEvaluar.Contains(vecino))
						{
							sinEvaluar.Add(vecino);
						}
					}
				}
		}
		return false;
	}

	//Auxiliar que reconstruye el camino de A*
	public void reconstruirCamino(Vertice inicio, Vertice final)
	{
		Vertice verticeActual = final;
		while (!verticeActual.Equals(inicio))
		{
			this.camino.Add(verticeActual);
			verticeActual.camino = verticeActual.padre;
			verticeActual = verticeActual.padre;
		}

		this.camino.Reverse();
	}

	float distancia(Vertice a, Vertice b)
	{
		return Vector3.Distance(a.posicion, b.posicion);
	}

	Vertice menorF(List<Vertice> lista, Vertice actualVertice)
	{

		Vertice verticeMenor = actualVertice;
		for (int i = 1; i < lista.Count; i++)
		{
			if (lista[i].fCost < verticeMenor.fCost ||
			    lista[i].fCost == verticeMenor.fCost && lista[i].hCost < verticeMenor.hCost)
			{
				verticeMenor = lista[i];
			}
		}
		return verticeMenor;
	}

	private bool esCaminable(Vertice inicio, Vertice final)
	{
		RaycastHit raycastFront;
		Vector3 direccion = inicio.posicion - final.posicion;
		float distancia = direccion.magnitude;
		
		if (Physics.Raycast(inicio.posicion, direccion, out raycastFront, distancia, obstaculos))
		{
			return false;
		}
		if (Physics.Raycast(final.posicion, -direccion, out raycastFront, distancia, obstaculos))
		{
			return false;
		}
		Collider[] fColliders = Physics.OverlapSphere(inicio.posicion + (direccion * distancia), 100f, obstaculos);
		if (fColliders.Length > 0)
		{
			return false;
		}

		return true;
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
