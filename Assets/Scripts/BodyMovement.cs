using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using Random = UnityEngine.Random;

public class BodyMovement : MonoBehaviour
{

    public float gobbleDuration;
    public float idleDuration;

    public Body body;
    private SpriteRenderer _spriteRenderer;
    private float _idleSpriteTimer;
    private bool _isGobbling;
    private readonly IEnumerator<int> _upAndDown = UpAndDownGenerator(3).GetEnumerator();

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = body.idleMiddle;
    }

    void Update()
    {
        if (!_isGobbling)
        {
            _spriteRenderer.sprite = GetInputDirectionSprite();
        }
    }

    public void Gobble(Direction direction)
    {
        _isGobbling = true;
        _spriteRenderer.sprite = GetGobbleSprite(direction);
        StartCoroutine(WaitCor(gobbleDuration));
    }

    private Sprite GetGobbleSprite(Direction direction)
    {
        if (direction == Direction.Left)
        {
            return body.openLeft;
        }

        if (direction == Direction.Middle)
        {
            return body.openMiddle;
        }

        return body.openRight;
    }

    private Sprite GetInputDirectionSprite()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            return body.openLeft;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            return body.openRight;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            return body.openMiddle;
        }

        _idleSpriteTimer += Time.deltaTime;
        if (_idleSpriteTimer >= idleDuration)
        {
            _idleSpriteTimer = 0f;
            _upAndDown.MoveNext();
            return body.IdleSprites[_upAndDown.Current];
        }

        return _spriteRenderer.sprite;
    }

    IEnumerator WaitCor(float totalShakeDuration)
    {
        float counter = 0f;
        while (counter < totalShakeDuration)
        {
            yield return null;
        }
        _isGobbling = false;
    }
}
