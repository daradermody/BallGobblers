using UnityEngine;

public class BallCatcher : MonoBehaviour {
    public GameObject levelManager;

    private SpriteRenderer _spriteRenderer;
    private FaceMovement _movementScript;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movementScript = GetComponent<FaceMovement>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (IsBall(other) && IsFalling(other)) {
            if (CaughtLeft(other) || CaughtRight(other) || CaughtMiddle(other)) {
                Destroy(other.gameObject);
                levelManager.GetComponent<LevelManager>().OnBallGobbled();
                _movementScript.Gobble(GetDirection(other));
            } else {
                levelManager.GetComponent<LevelManager>().OnBallMissed();
            }
        }
    }

    private static bool IsBall(Collider2D otherCollider) {
        return otherCollider.gameObject.GetComponent<BallMovement>() != null;
    }

    private static bool IsFalling(Collider2D other) {
        return other.gameObject.GetComponent<Rigidbody2D>().linearVelocity.y < 0;
    }

    private bool CaughtLeft(Collider2D other) {
        return _movementScript.GetSprites().openLeft == _spriteRenderer.sprite && other.gameObject.transform.position.x < -2;
    }

    private bool CaughtRight(Collider2D other) {
        return _movementScript.GetSprites().openRight == _spriteRenderer.sprite && other.gameObject.transform.position.x > 2;
    }

    private bool CaughtMiddle(Collider2D other) {
        var xPosition = other.gameObject.transform.position.x;
        return _movementScript.GetSprites().openMiddle == _spriteRenderer.sprite && xPosition < 1 && xPosition > -1;
    }

    private Direction GetDirection(Collider2D other) {
        if (CaughtLeft(other)) {
            return Direction.Left;
        }

        if (CaughtRight(other)) {
            return Direction.Right;
        }

        return Direction.Middle;
    }
}