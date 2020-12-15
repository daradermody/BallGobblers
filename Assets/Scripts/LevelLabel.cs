using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour {
    public Text levelLabel;
    public void UpdateLevel(int level) {
        levelLabel.text = level.ToString();
    }
}