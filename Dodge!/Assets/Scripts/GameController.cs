using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

    [Header("Score Display")]
    public TextMeshProUGUI scoreText;
    [Range(0f,100f)] public float timeToScore;

    [Header("Pause")]
    public GameObject pauseScreen;

    [Header("Health")]
    public Slider healthBar;
    [Range(1, 5)] public int maxLives;

    [Header("Camera Shake")]
    public Camera cam;
    [Range(0f, 1f)] public float shakeDuration;
    [Range(0f, 1f)] public float shakeMagnitude;

    public static int lives;
    public static float score;
        
    private bool isPaused = false;

    private void Start() {
        healthBar.maxValue = maxLives;
        healthBar.value = maxLives;
        lives = maxLives;

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
        lives--;
    }

    public IEnumerator CameraShake() {
        Vector3 originalPos = cam.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < shakeDuration) {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);
            float z = Random.Range(-shakeMagnitude, shakeMagnitude);

            cam.transform.localPosition += new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cam.transform.localPosition = originalPos;
    }
}
