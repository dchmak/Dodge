using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    
    public Vector3 offset;
    public float minZoom;
    public float maxZoom;
    public float zoomLimit;
    public float smoothTime;

    private GameObject player;
    private GameObject[] targets;
    private Vector3 velocity;
    private Camera cam;

    public static bool missileDestroyed;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        targets = GameObject.FindGameObjectsWithTag("Missile");

        cam = GetComponent<Camera>();
    }

    private void Update() {
        if (missileDestroyed) {
            targets = GameObject.FindGameObjectsWithTag("Missile");
        }
    }

    private void LateUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, smoothTime);

        Bounds bounds = new Bounds(player.transform.position, Vector3.zero);
        bounds.Encapsulate(player.transform.position);

        foreach (GameObject target in targets) {
            bounds.Encapsulate(target.transform.position);
        }

        //cam.fieldOfView = Mathf.Max(bounds.size.x, bounds.size.y);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Mathf.Lerp(maxZoom, minZoom, Mathf.Sqrt(Mathf.Pow(bounds.size.x, 2) + Mathf.Pow(bounds.size.y, 2)) / zoomLimit), Time.deltaTime);
    }

}
