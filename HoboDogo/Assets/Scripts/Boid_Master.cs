using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Master : MonoBehaviour {

    private List<GameObject> boids;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boid"))
        {
            boids.Add(other.gameObject);
        }
    }
}
