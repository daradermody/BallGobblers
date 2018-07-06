using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideThrow: MonoBehaviour {
    public float upThrust;
    public float forwardThrust;
    public float sideThrust;
    public Rigidbody ball;

    private void Start() {
        ball = GetComponent<Rigidbody>();
        ball.AddForce(
            (transform.forward * forwardThrust * 100) +
            (transform.up * upThrust * 100) + 
            (transform.right * sideThrust * 100)
        );
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}