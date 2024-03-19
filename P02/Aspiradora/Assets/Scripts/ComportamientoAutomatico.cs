using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico : MonoBehaviour {


    //Enum para los estados
    public enum State {
        MAPEO
    }

    private State currentState;
    private Sensores sensor;
	private Actuadores actuador;
	private Mapa mapa;


    void Start(){
        SetState(State.MAPEO);
        sensor = GetComponent<Sensores>();
		actuador = GetComponent<Actuadores>();
    }


    void FixedUpdate() {
        switch (currentState) {
            case State.MAPEO:
            UpdateMAPEO();
            break;
        }
    }

    // Funciones de actualizacion especificas para cada estado
    void UpdateMAPEO() {

    }

    // Función para cambiar de estado
    void SetState(State newState) {
        currentState = newState;
    }

}
