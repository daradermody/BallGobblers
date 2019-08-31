using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
    public Rigidbody2D ball;
    public float upThrust;
    public float sideThrust;

    private void Start() {
        ball = GetComponent<Rigidbody2D>();
        ball.AddForce(100 * upThrust * transform.up + 100 * sideThrust * transform.right);
    }
}