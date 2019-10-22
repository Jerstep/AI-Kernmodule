using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior 3D/Avoidance 3D")]
public class AvoidanceBehavior3D : FilteredFlockBehavior3D
{
    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        //if no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        //add all points together and avrage
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if(Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (agent.transform.position - item.position);
            }
        }
        if (nAvoid < 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
