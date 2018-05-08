/*
* Created by Daniel Mak
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameoverScoresController : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;

    private void Awake() {
        score.text = "Score: " + GameController.score.ToString("F0");
        highscore.text = "HighScore: " + PlayerPrefs.GetFloat("Highscore").ToString("F0");
    }
}
