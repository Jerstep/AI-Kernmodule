using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter 3D/Physics Layer 3D")]
public class PhysicsLayerFilter3D : ContextFilter3D
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent3D agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            if(mask == (mask | (1 << item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
