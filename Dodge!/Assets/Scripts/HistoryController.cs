/*
* Created by Daniel Mak
*/

using UnityEngine;
using TMPro;

public class HistoryController : MonoBehaviour {

    public TextMeshProUGUI highscore;

    private void Start () {
        highscore.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore", 0).ToString("F0");
	}
}