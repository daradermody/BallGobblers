using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CharacterSelection { Michael, Dara };

public class FaceMovement : MonoBehaviour {
    public CharacterSelection defaultCharacter;
    public Character[] characters;

    public float gobbleShakeDuration;
    public float gobbleShakeRadius;
    public float gobbleShakeAngle;
    public float idleDuration;

    [HideInInspector] public Character character;
    private SpriteRenderer _spriteRenderer;
    private float _idleSpriteTimer;
    private bool _isGobbling;
    private readonly IEnumerator<int> _upAndDown = UpAndDownGenerator(3).GetEnumerator();

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        character = characters[PlayerPrefs.HasKey("CharacterSelected") ? PlayerPrefs.GetInt("CharacterSelected") : (int) defaultCharacter];
        _spriteRenderer.sprite = character.idleMiddle;
    }

    void Update() {
        if (!_isGobbling) {
            _spriteRenderer.sprite = GetInputDirectionSprite();
        }
    }

    public void Gobble(Direction direction) {
        _isGobbling = true;
        _spriteRenderer.sprite = GetGobbleSprite(direction);
        StartCoroutine(ShakeGameObjectCor(_spriteRenderer.gameObject, gobbleShakeDuration));
    }

    private Sprite GetGobbleSprite(Direction direction) {
        if (direction == Direction.Left) {
            return character.gobbleLeft;
        }

        if (direction == Direction.Middle) {
            return character.gobbleMiddle;
        }

        return character.gobbleRight;
    }

    private Sprite GetInputDirectionSprite() {
        if (Input.GetAxis("Horizontal") < 0) {
            return character.openLeft;
        }

        if (Input.GetAxis("Horizontal") > 0) {
            return character.openRight;
        }

        if (Input.GetAxis("Vertical") > 0) {
            return character.openMiddle;
        }

        _idleSpriteTimer += Time.deltaTime;
        if (_idleSpriteTimer >= idleDuration) {
            _idleSpriteTimer = 0f;
            _upAndDown.MoveNext();
            return character.IdleSprites[_upAndDown.Current];
        }

        return _spriteRenderer.sprite;
    }

    IEnumerator ShakeGameObjectCor(GameObject objectToShake, float totalShakeDuration) {
        //Get Original Pos and rot
        Transform objTransform = objectToShake.transform;
        Vector3 defaultPos = objTransform.position;
        Quaternion defaultRot = objTransform.rotation;

        float counter = 0f;
        //Do the actual shaking
        while (counter < totalShakeDuration) {
            counter += Time.deltaTime;

            //Shake GameObject
            Vector3 tempPos = defaultPos + Random.insideUnitSphere * gobbleShakeRadius;
            tempPos.z = defaultPos.z;
            objTransform.position = tempPos;

            //Only Rotate the Z axis if 2D
            objTransform.rotation = defaultRot *
                                    Quaternion.AngleAxis(Random.Range(-gobbleShakeAngle, gobbleShakeAngle),
                                        new Vector3(0f, 0f, 1f));

            yield return null;
        }

        objTransform.position = defaultPos; //Reset to original postion
        objTransform.rotation = defaultRot; //Reset to original rotation

        _isGobbling = false;
    }

    private static IEnumerable<int> UpAndDownGenerator(int size) {
        var index = 0;
        var direction = 1;

        for (;;) {
            if (index == size - 1) {
                direction = -1;
            } else if (index == 0) {
                direction = 1;
            }

            index += direction;
            yield return index;
        }
    }
}
