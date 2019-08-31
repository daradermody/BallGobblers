using System;
using UnityEngine;

public class BallDestroy : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.GetComponent<BallMovement>() != null) {
            Destroy(otherCollider.gameObject);
        }
    }
}