using UnityEngine;

public class FaceMovement : MonoBehaviour {
    public Sprite leftSprite;
    public Sprite middleSprite;
    public Sprite rightSprite;

    private SpriteRenderer _spriteRenderer;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetAxis("Horizontal") > 0) {
            _spriteRenderer.sprite = rightSprite;
        } else if (Input.GetAxis("Horizontal") < 0) {
            _spriteRenderer.sprite = leftSprite;
        } else {
            _spriteRenderer.sprite = middleSprite;
        }
    }
}