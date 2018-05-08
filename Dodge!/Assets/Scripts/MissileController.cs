using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileController : MonoBehaviour {

    public GameObject explosionPrefab;

    [Range(0f, 10f)] public float speed = 1f;
    [Range(0f, 1000f)] public float angularSpeed = 1f;

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

            rb.angularVelocity = rotateAmount * angularSpeed;

            rb.velocity = transform.right * speed;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("Hit!");

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        CameraController.missileDestroyed = true;

        Transform particle = transform.GetChild(0);

        particle.parent = null;
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.Stop();
        
        audioController.Play("Explosion");

        Destroy(gameObject);
        Destroy(particle.gameObject, particleSystem.main.duration);
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
