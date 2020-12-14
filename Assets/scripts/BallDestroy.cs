using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallDestroy : MonoBehaviour {
    public Text currentScore;
    public Text finalScore;
    public Text finalScoreSuffix;
    public Image[] hearts;
    public Sprite[] heartSprites;
    public Camera mainCamera;
    public GameObject gameResultsMenuUi;
    public GameObject gameplayUi;
    private Image _lastHeart;
    private Color _endColor;

    private void Start() {
        _lastHeart = hearts[hearts.Length - 1];
        _endColor = mainCamera.backgroundColor;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.GetComponent<BallMovement>() != null) {
            Destroy(otherCollider.gameObject);
            StopCoroutine("ColourShiftBackground");
            StartCoroutine("ColourShiftBackground");
            StartCoroutine("DestroyHeart");
        }
    }

    private IEnumerator DestroyHeart() {
        Image nextHeart = Array.Find(hearts, heart => heart.enabled);

        for (int heartSpriteCell = 1; heartSpriteCell < heartSprites.Length; heartSpriteCell++) {
            nextHeart.sprite = heartSprites[heartSpriteCell];
            yield return new WaitForSeconds(0.025f);
        }

        nextHeart.enabled = false;
        
        if (nextHeart == _lastHeart) {
            ShowResultsScreen();
        }
    }

    IEnumerator ColourShiftBackground() {
        Color startColor = new Color(0.61f, 0f, 0f);
        
        for (float t = 0f; t <= 1.0; t += 0.1f) {
            mainCamera.backgroundColor = Color.Lerp(startColor, _endColor, t);
            yield return new WaitForSeconds(0.05f);
        }

        mainCamera.backgroundColor = _endColor;
    }

    private void ShowResultsScreen() {
        Time.timeScale = 0f;
        finalScore.text = currentScore.text;
        finalScoreSuffix.text = Int32.Parse(finalScore.text) == 1 ? "ball!" : "balls!";
        gameplayUi.SetActive(false);
        gameResultsMenuUi.SetActive(true);
    }
    
    public void TryAgain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}