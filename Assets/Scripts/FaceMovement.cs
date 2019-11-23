using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using Random = UnityEngine.Random;

public enum CharacterChoices { Michael, Dara };

public class FaceMovement : Movement {
    public CharacterChoices defaultCharacter;
    public Character[] characters;
    private Character characterSprites;

    void Start() {
        characterSprites = (Character) characters[PlayerPrefs.HasKey("CharacterSelected") ? PlayerPrefs.GetInt("CharacterSelected") : (int) defaultCharacter];
        SetSprites(characterSprites);
    }

    public void Gobble(Direction direction) {
        base.Gobble(GetGobbleSprite(direction), ShakeGameObjectCor(_spriteRenderer.gameObject, gobbleShakeDuration));
    }

    private Sprite GetGobbleSprite(Direction direction) {
        if (direction == Direction.Left) {
            return characterSprites.gobbleLeft;
        }

        if (direction == Direction.Middle) {
            return characterSprites.gobbleMiddle;
        }

        return characterSprites.gobbleRight;
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
}
