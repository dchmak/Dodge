using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileController : MonoBehaviour {

    public GameObject explosionPrefab;

    [Header("Initial Statistics")]
    [Range(0f, 10f)] public float speed = 1f;
    [Range(0f, 1000f)] public float angularSpeed = 100f;
    [Range(0f, 1000f)] public float bonusScore = 10f;

    [Header("Difficulty")]
    [Range(0, 1000)] public int scoreToProgress = 200;
    [Range(0f, 10f)] public float speedDifficultyScale = 1f;
    [Range(0f, 1000f)] public float angularSpeedDifficultyScale = 100f;

    private Transform target;
    private Rigidbody2D rb;
    private AudioController audioController;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != 0) {
            target = GetClosestObject(players);
        }

        audioController = FindObjectOfType<AudioController>();
    }

    private void Update() {
        //audioController.Play("Engine");
    }

    void FixedUpdate () {
        if (target != null) {
            Vector2 dir = ((Vector2)target.position - rb.position).normalized;

            float rotateAmount = Vector3.Cross(transform.right, dir).z;
            
            rb.angularVelocity = rotateAmount * (angularSpeed +( (int)GameController.score / scoreToProgress) * angularSpeedDifficultyScale);

            rb.velocity = transform.right * (speed + ( (int)GameController.score / scoreToProgress) * speedDifficultyScale);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("Hit!");

        GameController gameController = FindObjectOfType<GameController>();

        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Animator>().Play("TakeDamage");

            gameController.TakeDamage();
            StartCoroutine(gameController.CameraShake());
        }

        if (collision.gameObject.tag == "Missile") {
            gameController.ChangeScore(bonusScore);
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        CameraController.needUpdate = true;

        Transform particle = transform.GetChild(0);

        particle.parent = null;
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.Stop();
        
        audioController.Play("Explosion");

        Destroy(gameObject);
        Destroy(particle.gameObject, particleSystem.main.duration);

        PlayerController.isSlowed = false;
    }

    Transform GetClosestObject(GameObject[] obj) {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in obj) {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}
