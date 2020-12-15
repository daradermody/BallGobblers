using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultsMenu : MonoBehaviour {
    public Text finalScore;
    public Text finalScoreSuffix;

    public void ShowResultsScreen(int score) {
        finalScore.text = score.ToString();
        finalScoreSuffix.text = score == 1 ? "ball!" : "balls!";
        gameObject.SetActive(true);
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