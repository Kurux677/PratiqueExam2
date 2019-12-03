using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    private Rigidbody2D rbd;
	// Use this for initialization
	void Start () {
        rbd = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddForceX(float forceX)
    {
        rbd.AddForce(Vector2.right * forceX);
    }

    public void AddForceY(float forceY)
    {
        rbd.AddForce(Vector2.up* forceY);
    }
}
