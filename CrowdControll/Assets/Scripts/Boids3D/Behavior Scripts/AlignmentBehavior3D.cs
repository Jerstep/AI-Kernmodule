using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior 3D/Alignment 3D")]
public class AlignmentBehavior3D : FilteredFlockBehavior3D
{
    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        //if no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        //add all points together and avrage
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.transform.forward;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
