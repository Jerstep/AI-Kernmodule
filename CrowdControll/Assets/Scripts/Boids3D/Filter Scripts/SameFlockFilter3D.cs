using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter 3D/Same Flock 3D")]
public class SameFlockFilter3D : ContextFilter3D
{
    public override List<Transform> Filter(FlockAgent3D agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in original)
        {
            FlockAgent3D itemAgent = item.GetComponent<FlockAgent3D>();
            if(itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
