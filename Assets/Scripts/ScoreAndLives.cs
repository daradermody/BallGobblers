using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndLives : MonoBehaviour {
    public Text scoreLabel;
    public Image[] hearts;
    public Sprite[] heartSprites;
    private int _heartsLeft;
    private Color _endColor;

    private void Start() {
        _heartsLeft = hearts.Length;
    }

    public void UpdateScore(int score) {
        scoreLabel.text = score.ToString();
    }

    public int DestroyHeart() {
        StartCoroutine("DestroyHeartAnimation");
        _heartsLeft -= 1;
        return _heartsLeft;
    }

    private IEnumerator DestroyHeartAnimation() {
        Image nextHeart = Array.Find(hearts, heart => heart.enabled);

        for (int heartSpriteCell = 1; heartSpriteCell < heartSprites.Length; heartSpriteCell++) {
            nextHeart.sprite = heartSprites[heartSpriteCell];
            yield return new WaitForSeconds(0.025f);
        }

        nextHeart.enabled = false;
    }
}