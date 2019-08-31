using System;
using UnityEngine;
using UnityEngine.UI;

public class BallCatcher : MonoBehaviour {
    public Text score;

    private SpriteRenderer _spriteRenderer;
    private FaceMovement _movementScript;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movementScript = GetComponent<FaceMovement>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (IsBall(other) && IsFalling(other) && (CaughtLeft(other) || CaughtRight(other) || CaughtMiddle(other))) {
            Destroy(other.gameObject);
            score.text = (Int32.Parse(score.text) + 1).ToString();
        }
    }

    private static bool IsBall(Collider2D otherCollider) {
        return otherCollider.gameObject.GetComponent<BallMovement>() != null;
    }

    private static bool IsFalling(Collider2D other) {
        return other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0;
    }

    private bool CaughtLeft(Collider2D other) {
        return _movementScript.leftSprite == _spriteRenderer.sprite && other.gameObject.transform.position.x < -2;
    }

    private bool CaughtRight(Collider2D other) {
        return _movementScript.rightSprite == _spriteRenderer.sprite && other.gameObject.transform.position.x > 2;
    }

    private bool CaughtMiddle(Collider2D other) {
        var xPosition = other.gameObject.transform.position.x;
        return _movementScript.middleSprite == _spriteRenderer.sprite && xPosition < 1 && xPosition > -1;
    }
}