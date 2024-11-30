using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CelestialBody : MonoBehaviour
{
    public float mass;
    public Vector3 initialVelocity;
    public Vector3 velocity;

    public Vector3 initialPosition;

    public bool attractOthers;
    public bool isAttracted;

    void Start()
    {
        velocity = initialVelocity;
        initialPosition = transform.position;
    }

    public void UpdateVelocity (CelestialBody[] allBodies, float timeStep)
    {
        if (isAttracted)
        {
            foreach (CelestialBody otherBody in allBodies)
            {
                if (otherBody != this && otherBody.attractOthers)
                {
                    float sqrDst = (otherBody.transform.position - transform.position).sqrMagnitude;
                    Vector3 forceDir = (otherBody.transform.position - transform.position).normalized;

                    Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherBody.mass / sqrDst;
                    Vector3 acceleration = force / mass;
                    
                    velocity += acceleration * timeStep;
                }
            }
        }
    }

    public void UpdatePosition(float timeStep)
    {
        transform.position += velocity * timeStep;
    }            
            
    

    

}
