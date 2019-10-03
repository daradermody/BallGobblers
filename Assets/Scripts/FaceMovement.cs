using UnityEngine;
using System;
using System.Collections;

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

    private Sprite[] _idleSprites;

    public float gobbleDuration;
    public float gobbleShakeDuration;
    public float gobbleShakeRadius;
    public float gobbleShakeAngle;

    public float idleDuration;

    private SpriteRenderer _spriteRenderer;
    private float idleSpriteTimer;
    private bool isGobbling;
    private float gobbleSpriteTimer;
    private bool shaking = false;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _idleSprites = new Sprite[3] { idleLeftSprite, idleMiddleSprite, idleRightSprite };
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
        shakeGameObject(_spriteRenderer.gameObject, gobbleShakeDuration);
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

    IEnumerator shakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration)
    {
        //Get Original Pos and rot
        Transform objTransform = objectToShake.transform;
        Vector3 defaultPos = objTransform.position;
        Quaternion defaultRot = objTransform.rotation;

        float counter = 0f;
        //Do the actual shaking
        while (counter < totalShakeDuration) {
            counter += Time.deltaTime;

            //Shake GameObject
            Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * gobbleShakeRadius;
            tempPos.z = defaultPos.z;
            objTransform.position = tempPos;

            //Only Rotate the Z axis if 2D
            objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-gobbleShakeAngle, gobbleShakeAngle), new Vector3(0f, 0f, 1f));
            
            yield return null;
        }
        objTransform.position = defaultPos; //Reset to original postion
        objTransform.rotation = defaultRot;//Reset to original rotation

        shaking = false;
    }


    void shakeGameObject(GameObject objectToShake, float shakeDuration)
    {
        if (shaking) {
            return;
        }
        shaking = true;
        StartCoroutine(shakeGameObjectCOR(objectToShake, shakeDuration));
    }
}