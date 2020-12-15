using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelDeclaration : MonoBehaviour {
    public GameObject parent;
    public Text levelLabel;
    public float timeMoving;
    public float timeStill;

    private Vector3 _startPosition;
    private Vector3 _middlePosition;
    private Vector3 _endPosition;
    private float _startTime;

    private void Awake() {
        Vector3 position = transform.position;
        _startPosition = new Vector3(position.x + Screen.width, position.y, position.z);
        _middlePosition = new Vector3(position.x, position.y, position.z);
        _endPosition = new Vector3(-Screen.width, position.y, position.z);
    }

    public void DeclareLevel(int level, float timeLabelMoving = 0.5f, float timeLabelStill = 1f) {
        levelLabel.text = level.ToString();
        timeMoving = timeLabelMoving;
        timeStill = timeLabelStill;

        transform.position = _startPosition;
        parent.SetActive(true);

        _startTime = Time.time;
    }
    
    void Update() {
        float lifetime = Time.time - _startTime;
        if (lifetime < timeMoving) {
            transform.position = Vector3.Lerp(_startPosition, _middlePosition, lifetime / timeMoving);
        } else if (lifetime < timeMoving + timeStill) {
            transform.position = _middlePosition;
        } else if (lifetime < timeMoving + timeStill + timeMoving) {
            transform.position = Vector3.Lerp(_middlePosition, _endPosition, (lifetime - timeMoving - timeStill) / timeMoving);
        } else {
            parent.SetActive(false);
        }
    }
}
