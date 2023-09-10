using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evades : SteeringBase
{
    public Transform perseguidor;
    public float maxspeed = 5.0f;
    Vector3 ToEvader;
    private float tiempoPredicci�n = 2f;

    //Variables Varias
    Vector3 distancia;

    // Update is called once per frame
    public override Vector3 CalcularSteering()
    {
        ToEvader = perseguidor.position - MiRigidbody.position;

        float anticipacion = ToEvader.magnitude / maxspeed;

        Vector3 predictedPosition = perseguidor.position + perseguidor.GetComponent<Rigidbody>().velocity * anticipacion * tiempoPredicci�n;

        // Calcula la direcci�n deseada para dirigirse hacia la posici�n predicha del perseguidor
        Vector3 velocidadDeseada = (predictedPosition - MiRigidbody.position).normalized * maxspeed;

        Vector3 steeringForce = velocidadDeseada - MiRigidbody.velocity;

        return steeringForce;
    }
}

