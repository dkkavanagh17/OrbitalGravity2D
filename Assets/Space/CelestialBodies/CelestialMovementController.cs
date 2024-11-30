using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialMovementController : MonoBehaviour
{
    public void FixedUpdate()
    {
        CelestialBody[] bodies = GameObject.FindObjectsOfType<CelestialBody>();

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdateVelocity (bodies, Universe.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(Universe.physicsTimeStep);
        }

    }
}
