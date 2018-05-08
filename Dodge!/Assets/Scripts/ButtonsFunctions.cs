/*
* Created by Daniel Mak
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour {

    public void LoadScene(string name) {
        if (Application.CanStreamedLevelBeLoaded(name)) {
            SceneManager.LoadSceneAsync(name);
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void SetScore() {
        float score = FindObjectOfType<GameController>().getScore();

        if (PlayerPrefs.GetFloat("Highscore", 0) < score) {
            PlayerPrefs.SetFloat("Highscore", score);
        }
    }

    public void ResetAll() {
        PlayerPrefs.DeleteAll();
    }
}