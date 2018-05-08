using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    [Range(0f,100f)] public float timeToScore;

    public GameObject pauseScreen;

    public Slider healthBar;
    [Range(1, 5)] public int maxLives;
    
    public static float score;

    private bool isPaused = false;

    private void Start() {
        healthBar.maxValue = maxLives;
        healthBar.value = maxLives;

        score = 0f;
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (isPaused) Unpause();
            else Pause();
        }
    }

    private void LateUpdate () {
        score += Time.deltaTime * timeToScore;

        scoreText.text = "Score: " + score.ToString("F0");

        if (healthBar.value == 0) {
            PlayerPrefs.SetFloat("Highscore", score);

            SceneManager.LoadScene("Gameover");
        }
	}

    public float GetScore() {
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

    public void TakeDamage() {
        healthBar.value--;

        //Debug.Log(lives);
    }
}
