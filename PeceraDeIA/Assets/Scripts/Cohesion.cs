using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : SteeringBase
{
    public float radioCohesion = 5f;
    public LayerMask capaAgentes;
    public float speed = 5f;
    public float distanciaMinima = 2f; // Distancia m�nima para evitar colisiones

    public override Vector3 CalcularSteering()
    {
        Vector3 centroDeLaMasa = Vector3.zero;
        Vector3 avoidanceForce = Vector3.zero;
        int numeroDeVecinos = 0;

        Collider[] agentesCercanos = Physics.OverlapSphere(transform.position, radioCohesion, capaAgentes);

        foreach (Collider agente in agentesCercanos)
        {
            if (agente.gameObject != gameObject)
            {
                // Calcular la direcci�n hacia el agente
                Vector3 direccion = agente.transform.position - transform.position;
                float distancia = direccion.magnitude;

                // Si la distancia es menor que la distancia m�nima, calcular una fuerza de separaci�n
                if (distancia < distanciaMinima)
                {
                    avoidanceForce += -direccion.normalized * (distanciaMinima - distancia);
                }
                else
                {
                    // Sumar la posici�n para la cohesi�n
                    centroDeLaMasa += agente.transform.position;
                    numeroDeVecinos++;
                }
            }
        }

        if (numeroDeVecinos > 0)
        {
            // Calcular el centro de la masa como el promedio de las posiciones
            centroDeLaMasa /= numeroDeVecinos;
        }

        // Calcular la fuerza de direcci�n para la cohesi�n
        Vector3 cohesionForce = Seek(centroDeLaMasa);

        // Combinar la fuerza de cohesi�n con la fuerza de separaci�n
        Vector3 steeringForce = cohesionForce + avoidanceForce;

        return steeringForce;
    }

    private Vector3 Seek(Vector3 posicion)
    {
        Vector3 direccion = (posicion - transform.position).normalized;
        Vector3 velocidad = direccion * speed;
        Vector3 vectorSteering = velocidad - MiRigidbody.velocity;

        return vectorSteering;
    }
}
