using System;
using UnityEngine;

[Serializable]
public class Character {
    public Sprite idleLeft;
    public Sprite idleMiddle;
    public Sprite idleRight;
    public Sprite gobbleLeft;
    public Sprite gobbleMiddle;
    public Sprite gobbleRight;
    public Sprite openLeft;
    public Sprite openMiddle;
    public Sprite openRight;

    public Sprite[] IdleSprites => new[] {idleLeft, idleMiddle, idleRight};
}

public enum Direction { Left, Middle, Right }