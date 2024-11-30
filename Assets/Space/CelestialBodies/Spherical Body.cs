using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]

public class SphericalBody : MonoBehaviour
{
    public float radius;
    public float orbitTrailWidth;
    public float orbitTrailTime;

    void OnValidate()
    {
        gameObject.transform.localScale = Vector3.one * radius;
        
        var pathColour = gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
        var trailRenderer = gameObject.GetComponentInChildren<TrailRenderer>();

        trailRenderer.widthMultiplier = orbitTrailWidth;
        trailRenderer.time = orbitTrailTime;
        
        Gradient gradient = new Gradient();
        gradient.SetKeys (new GradientColorKey[] {new GradientColorKey(pathColour, 0.0f), new GradientColorKey(pathColour, 1.0f) }, 
        new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );
        
        trailRenderer.colorGradient = gradient;

    }
}
