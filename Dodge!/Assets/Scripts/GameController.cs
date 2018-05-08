using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    [Range(0f,100f)] public float timeToScore;

    public GameObject pauseScreen;
    
    private float score;
    private bool isPaused = false;

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (isPaused) Unpause();
            else Pause();
        }
    }

    private void LateUpdate () {
        score += Time.deltaTime * timeToScore;

        scoreText.text = "Score: " + score.ToString("F0");
	}

    public float getScore() {
        return score;
    }

    public void ChangeScore(float change) {
        score += change;
    }

    public void Pause() {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Unpause() {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
