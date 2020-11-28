using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject mainMenuPage;
    public GameObject characterSelectionPage;

    public void Start() {
        characterSelectionPage.SetActive(false);
        mainMenuPage.SetActive(true);
    }

    public void PlayGame(int character) {
        PlayerPrefs.SetInt("CharacterSelected", character);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}