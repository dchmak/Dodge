using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour {

    [Range(0f, 10f)] public float speed = 1f;
    [Range(0f, 10f)] public float emergencySpeed = 3f;

    [Range(0f, 1f)] public float cautionRange = 0.5f;
    [Range(0f, 1f)] public float slowTime;

    private Rigidbody2D rb;
    private CircleCollider2D cir;
    private bool isSlowed;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cir = GetComponent<CircleCollider2D>();

        cir.radius = cautionRange;
        isSlowed = false;
    }

    private void Update() {
        if (GameController.lives == 1) cir.enabled = true;
        else cir.enabled = true;

        if (isSlowed) Time.timeScale = slowTime;
        else Time.timeScale = 1f;
    }

    void FixedUpdate () {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(xMovement, yMovement).normalized;

        if (isSlowed) rb.velocity = dir * emergencySpeed;
        else rb.velocity = dir * speed;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (GameController.lives == 1 && collision.gameObject.tag == "Missile") {
            isSlowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (isSlowed) {
            isSlowed = false;
        }
    }
}
