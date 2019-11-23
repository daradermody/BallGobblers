using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Movement : MonoBehaviour
{
    public float gobbleShakeDuration;
    public float gobbleShakeRadius;
    public float gobbleShakeAngle;
    public float idleDuration;

    protected SpriteRenderer _spriteRenderer;
    protected float _idleSpriteTimer;
    protected bool _isGobbling;

    private MovementSprites sprites;

    private readonly IEnumerator<int> _upAndDown = UpAndDownGenerator(3).GetEnumerator();

    protected void SetSprites(MovementSprites sprites) {
        this.sprites = sprites;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = this.sprites.idleMiddle;
    }

    public MovementSprites GetSprites() {
        return sprites;
    }

    void Update() {
        if (!_isGobbling) {
            _spriteRenderer.sprite = GetInputDirectionSprite();
        }
    }

    private Sprite GetInputDirectionSprite() {
        if (Input.GetAxis("Horizontal") < 0) {
            return sprites.openLeft;
        }

        if (Input.GetAxis("Horizontal") > 0) {
            return sprites.openRight;
        }

        if (Input.GetAxis("Vertical") > 0) {
            return sprites.openMiddle;
        }

        _idleSpriteTimer += Time.deltaTime;
        if (_idleSpriteTimer >= idleDuration) {
            _idleSpriteTimer = 0f;
            _upAndDown.MoveNext();
            return sprites.IdleSprites[_upAndDown.Current];
        }

        return _spriteRenderer.sprite;
    }

    public void Gobble(Sprite sprite, IEnumerator coroutine) {
        _isGobbling = true;
        _spriteRenderer.sprite = sprite;
        StartCoroutine(coroutine);
    }

}
