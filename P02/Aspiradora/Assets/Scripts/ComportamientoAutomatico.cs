using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico : MonoBehaviour {


    //Enum para los estados
    public enum State {
        MAPEO,
        DFS
    }

    private State currentState;
    private Sensores sensor;
	private Actuadores actuador;
	private Mapa mapa;
    private Vertice verticeActual, verticeDestino;
    public bool fp = true, look;
    private bool turnLeft= false, seDetuvo = false;
    


    void Start(){
        SetState(State.DFS);
        sensor = GetComponent<Sensores>();
		actuador = GetComponent<Actuadores>();
        mapa = GetComponent<Mapa>();
        mapa.ColocarNodo(0);
        mapa.popStack(out verticeActual);
    }


    void FixedUpdate() {
        switch (currentState) {
            case State.MAPEO:
            UpdateMAPEO();
            break;
            case State.DFS:
            UpdateDFS();
            break;
        }


    }

    // Funciones de actualizacion especificas para cada estado
    /**
     * PASOS PARA EL DFS
     * 1.- Colocar un vértice (meterlo a la pila 'ColocarNodo' ya lo mete a la pila
     * 2.- Sacar de la pila, e intentar poner mas vértices
     * 3.- Hacer backtrack al siguiente vértice en la pila
     * 4.- Repetir hasta vaciar la pila
     */
    void UpdateMAPEO()
    {

        if (sensor.Bateria() <= 10 && sensor.Bateria() > 0)
        {

            actuador.Detener();
            verticeDestino = mapa.mapa.grafica[0];
            mapa.clearPath();
            mapa.mapa.AStar(mapa.getVerticeInPosition(4), verticeDestino);

        }
        if (fp) {
            mapa.popStack(out verticeActual);
            mapa.setPreV(verticeActual);   //Asignar a mapa el vértice nuevo al que nos vamos a mover, para crear las adyacencias necesarias.
            fp = false;
        }
        
        if (Vector3.Distance(sensor.Ubicacion(), verticeActual.posicion) >= 0.04f) {
            if (!look) {
                transform.LookAt(verticeActual.posicion);
                look = true;
            }
            //actuador.Adelante();
            moverse();
        } else {
            look = false;
            fp = true;
            SetState(State.DFS);
        }
        if (!mapa.isEmptyStack())
        {
            try
            {
                verticeActual = mapa.getVerticeInPosition(4);
            }
            catch (Exception e)
            {
                verticeActual = mapa.mapa.grafica[mapa.mapa.grafica.Count - 1];
            }
            mapa.setPreV(verticeActual);
        }
        SetState(State.DFS);
    } 

    // Funciones de actualizacion especificas para cada estado
    void UpdateDFS() {
        if (!sensor.FrenteLibre()) {
            actuador.Detener();
        }
        if (sensor.IzquierdaLibre()) {
            mapa.ColocarNodo(1);
        }
        if (sensor.DerechaLibre()) {
            mapa.ColocarNodo(3);
        }
        if (sensor.FrenteLibre()) {
            mapa.ColocarNodo(2);
        }
        SetState(State.MAPEO);
    }

    // Función para cambiar de estado
    void SetState(State newState) {
        currentState = newState;
    }

    void moverse()
    {
        if (sensor.Bateria() <= 0.00)
        {
            actuador.Detener();
        }
        if (sensor.FrenteLibre())
        {
            actuador.Adelante();

        }
        else if (!sensor.FrenteLibre() && !seDetuvo)
        {
            actuador.Detener();
            seDetuvo = true;
            
        }
        else if (turnLeft && sensor.DerechaLibre())
        {
            actuador.GirarDerecha();
            actuador.Adelante();
            turnLeft = false;
            
        }
        else if (!turnLeft && sensor.IzquierdaLibre())
        {
            actuador.GirarIzquierda();
            actuador.Adelante();
            turnLeft = true;
        }
        else if (sensor.DerechaLibre())
        {

            actuador.GirarDerecha();
            actuador.Adelante();
            turnLeft = false;
            
        }
        else if (sensor.IzquierdaLibre())
        {
            actuador.GirarIzquierda();
            actuador.Adelante();
            turnLeft = true;
        }
        else if (!sensor.FrenteLibre() && !sensor.DerechaLibre() && !sensor.IzquierdaLibre())
        {
            actuador.Detener();
            actuador.GirarDerecha();
            actuador.GirarDerecha();
            actuador.Adelante();
        }
    }
}