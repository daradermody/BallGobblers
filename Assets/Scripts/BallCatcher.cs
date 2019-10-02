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
            _movementScript.Gobble(GetDirection(CaughtLeft(other), CaughtRight(other)));
        }
    }

    private static bool IsBall(Collider2D otherCollider) {
        return otherCollider.gameObject.GetComponent<BallMovement>() != null;
    }

    private static bool IsFalling(Collider2D other) {
        return other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0;
    }

    private bool CaughtLeft(Collider2D other) {
        return _movementScript.openLeftSprite == _spriteRenderer.sprite && other.gameObject.transform.position.x < -2;
    }

    private bool CaughtRight(Collider2D other) {
        return _movementScript.openRightSprite == _spriteRenderer.sprite && other.gameObject.transform.position.x > 2;
    }

    private bool CaughtMiddle(Collider2D other) {
        var xPosition = other.gameObject.transform.position.x;
        return _movementScript.openMiddleSprite == _spriteRenderer.sprite && xPosition < 1 && xPosition > -1;
    }

    private int GetDirection(bool left, bool right) {
        if (left) {
            return 0;
        } else if (right) {
            return 1;
        } else {
            return 2;
        }
    }

}