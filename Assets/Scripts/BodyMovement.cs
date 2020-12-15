using System.Collections;
using UnityEngine;

public class BodyMovement : Movement {

    public MovementSprites body;

    void Start() {
        SetSprites(body);
    }

    public void Gobble(Direction direction) {
        base.Gobble(GetGobbleSprite(direction), WaitCor(gobbleShakeDuration));
    }
    
    private Sprite GetGobbleSprite(Direction direction) {
        if (direction == Direction.Left) {
            return body.openLeft;
        }

        if (direction == Direction.Middle) {
            return body.openMiddle;
        }

        return body.openRight;
    }
    
    IEnumerator WaitCor(float totalShakeDuration) {
        yield return new WaitForSeconds(totalShakeDuration);
        isGobbling = false;
    }
}
