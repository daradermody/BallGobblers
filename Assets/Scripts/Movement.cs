using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Movement : MonoBehaviour {
    public float gobbleShakeDuration;
    public float gobbleShakeRadius;
    public float gobbleShakeAngle;
    public float idleDuration;

    protected SpriteRenderer spriteRenderer;
    protected float idleSpriteTimer;
    protected bool isGobbling;

    private MovementSprites _sprites;

    private readonly IEnumerator<int> _upAndDown = UpAndDownGenerator(3).GetEnumerator();

    protected void SetSprites(MovementSprites sprites) {
        this._sprites = sprites;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = this._sprites.idleMiddle;
    }

    public MovementSprites GetSprites() {
        return _sprites;
    }

    void Update() {
        if (!isGobbling) {
            spriteRenderer.sprite = GetInputDirectionSprite();
        }
    }

    private Sprite GetInputDirectionSprite() {
        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetMouseButton(0) && (Input.mousePosition.x / Screen.width) < 0.33) {
            return _sprites.openLeft;
        }

        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetMouseButton(0) && (Input.mousePosition.x / Screen.width) < 0.66) {
            return _sprites.openMiddle;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetMouseButton(0) && (Input.mousePosition.x / Screen.width) < 1) {
            return _sprites.openRight;
        }

        idleSpriteTimer += Time.deltaTime;
        if (idleSpriteTimer >= idleDuration) {
            idleSpriteTimer = 0f;
            _upAndDown.MoveNext();
            return _sprites.IdleSprites[_upAndDown.Current];
        }

        return spriteRenderer.sprite;
    }

    protected void Gobble(Sprite sprite, IEnumerator coroutine) {
        isGobbling = true;
        spriteRenderer.sprite = sprite;
        StartCoroutine(coroutine);
    }
}