using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MovementSprites {
    public Sprite idleLeft;
    public Sprite idleMiddle;
    public Sprite idleRight;
    public Sprite openLeft;
    public Sprite openMiddle;
    public Sprite openRight;

    public Sprite[] IdleSprites => new[] { idleLeft, idleMiddle, idleRight };
}

[Serializable]
public class Character : MovementSprites {
    public Sprite gobbleLeft;
    public Sprite gobbleMiddle;
    public Sprite gobbleRight;
}

public enum Direction { Left, Middle, Right }

public static class Utils {
    public static IEnumerable<int> UpAndDownGenerator(int size) {
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
