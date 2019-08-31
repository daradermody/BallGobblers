using System;
using UnityEngine;
using UnityEngine.UI;

public class BallDestroy : MonoBehaviour {
    public Text score;

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.GetComponent<BallMovement>() != null) {
            Destroy(otherCollider.gameObject);
            score.text = (Int32.Parse(score.text) - 1).ToString();
        }
    }
}