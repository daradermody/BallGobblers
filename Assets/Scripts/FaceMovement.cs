using UnityEngine;
using System;

public class FaceMovement : MonoBehaviour {

    public Sprite idleLeftSprite;
    public Sprite idleMiddleSprite;
    public Sprite idleRightSprite;
    
    public Sprite openLeftSprite;
    public Sprite openMiddleSprite;
    public Sprite openRightSprite;

    public Sprite closedLeftSprite;
    public Sprite closedMiddleSprite;
    public Sprite closedRightSprite;

    public Sprite[] _idleSprites;

    public float gobbleDuration;
    public float idleDuration;

    private SpriteRenderer _spriteRenderer;
    private float idleSpriteTimer;
    private bool isGobbling;
    private float gobbleSpriteTimer;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (isGobbling) {
            gobbleSpriteTimer += Time.deltaTime;
            if (gobbleSpriteTimer >= gobbleDuration)
            {
                gobbleSpriteTimer = 0f;
                isGobbling = false;
                _spriteRenderer.sprite = GetInputDirectionSprite();
            }
        } else {
            _spriteRenderer.sprite = GetInputDirectionSprite();
        }
    }

    public void Gobble(int direction) {
        isGobbling = true;
        _spriteRenderer.sprite = GetGobbleSprite(direction);
    }

    private Sprite GetNextIdleSprite(Sprite currentSprite) {
        int currentIndex = Array.IndexOf(_idleSprites, currentSprite);
        int nextIndex = currentIndex + 1;
        if (nextIndex + 1 <= _idleSprites.Length) {
            return _idleSprites[nextIndex];
        } else {
            Array.Reverse(_idleSprites, 0, _idleSprites.Length);
        }
        return _idleSprites[1];
    }

    private Sprite GetGobbleSprite(int direction) {
        switch (direction) {
            case 0:
                return closedLeftSprite;
            case 1:
                return closedRightSprite;
            case 2:
                return closedMiddleSprite;
            default:
                Debug.Log("Invalid direction: " + direction);
                return closedMiddleSprite;
        }
    }

    private Sprite GetInputDirectionSprite() {
        if (Input.GetAxis("Horizontal") > 0) {
            return openRightSprite;
        }
        else if (Input.GetAxis("Horizontal") < 0) {
            return openLeftSprite;
        }
        else if ((Input.GetAxis("Vertical") > 0)) {
            return openMiddleSprite;
        }
        
        idleSpriteTimer += Time.deltaTime;
        if (idleSpriteTimer >= idleDuration) {
            idleSpriteTimer = 0f;
            return GetNextIdleSprite(_spriteRenderer.sprite);
        }

        return _spriteRenderer.sprite;
    }
}