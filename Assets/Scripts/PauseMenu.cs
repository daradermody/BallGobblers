using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuUi;
    private bool gameIsPaused = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void PauseGame() {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Debug.Log("Quitting!");
        Application.Quit();
    }
}