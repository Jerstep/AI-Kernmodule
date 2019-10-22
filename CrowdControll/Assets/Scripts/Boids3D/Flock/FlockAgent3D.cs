using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent3D : MonoBehaviour
{
    Flock3D agentFlock;
    public Flock3D AgentFlock { get { return agentFlock; } } 
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }


    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock3D flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity; // For 3D use forward
        transform.position += velocity * Time.deltaTime;
    }
}
