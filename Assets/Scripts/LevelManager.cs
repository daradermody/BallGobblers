using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public GameObject levelDeclarationUi;
    public GameObject scoreAndLives;
    public GameObject mainCamera;
    public GameObject gameResultsMenuUi;
    public GameObject gameplayUi;
    public GameObject ballGenerator;
    public GameObject levelLabel;

    private int _score;
    private int _level = 1;

    void Start() {
        gameplayUi.SetActive(false);
        levelDeclarationUi.GetComponent<LevelDeclaration>().DeclareLevel(_level);
        StartCoroutine("StartBallGeneration", 2f);
    }

    IEnumerator StartBallGeneration(float delay) {
        yield return new WaitForSeconds(delay);
        gameplayUi.SetActive(true);
        ballGenerator.GetComponent<BallGenerator>().StartGeneration();
    }

    public void OnBallMissed() {
        var heartsLeft = scoreAndLives.GetComponent<ScoreAndLives>().DestroyHeart();
        if (heartsLeft > 0) {
            mainCamera.GetComponent<CameraManager>().ColourShiftBackground(new Color(0.61f, 0f, 0f));
        } else {
            Time.timeScale = 0f;
            gameplayUi.SetActive(false);
            gameResultsMenuUi.GetComponent<GameResultsMenu>().ShowResultsScreen(_score);
        }
    }

    public void OnBallGobbled() {
        _score += 1;
        scoreAndLives.GetComponent<ScoreAndLives>().UpdateScore(_score);

        if (_score % 20 == 0) {
            IncrementLevel();
        }
    }

    void IncrementLevel() {
        _level += 1;
        ballGenerator.GetComponent<BallGenerator>().StopGeneration();
        gameplayUi.SetActive(false);
        levelDeclarationUi.GetComponent<LevelDeclaration>().DeclareLevel(_level);
        levelLabel.GetComponent<LevelLabel>().UpdateLevel(_level);
        StartCoroutine("StartBallGeneration", 2f);
    }
}