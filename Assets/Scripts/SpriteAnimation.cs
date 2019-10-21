using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Utils;

public class SpriteAnimation : MonoBehaviour {
    public Sprite[] sprites;
    private Image _imageComponent;

    void Start() {
        _imageComponent = GetComponent<Image>();
        StartCoroutine(ChangeSprite());
    }

    private IEnumerator  ChangeSprite() {
        foreach (int index in UpAndDownGenerator(sprites.Length)) {
            _imageComponent.sprite = sprites[index];
            yield return new WaitForSeconds(Random.Range(0.2f, 2f));
        }
    } 
}