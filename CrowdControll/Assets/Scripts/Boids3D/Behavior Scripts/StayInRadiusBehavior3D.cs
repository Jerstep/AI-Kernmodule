using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior 3D/Stay In Radius 3D")]
public class StayInRadiusBehavior3D : FlockBehavior3D
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f)
        {
            return Vector3.zero;
        }
        Debug.Log(center);
        return centerOffset * t * t;
    }
}
