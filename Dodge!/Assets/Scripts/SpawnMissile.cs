using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile : MonoBehaviour {

    [Range(0f, 1f)] public float spawnRate;
    [Range(0f, 10f)] public float spawnRadius;
    public GameObject spawnerPrefab;

    private Pooler pooler;
    private float nextTimeToSpawn = 0f;

    private void Start() {
        pooler = Pooler.Instance;
    }

    private void Update () {
        if (spawnRate != 0 && Time.time >= nextTimeToSpawn) {
            nextTimeToSpawn = Time.time + 1f / spawnRate;

            Vector3 spawnOffset = Random.insideUnitCircle.normalized * spawnRadius;

            GameObject spawner = Instantiate(spawnerPrefab, transform.position + spawnOffset, Quaternion.identity);
            Destroy(spawner, spawner.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            GameObject missile = pooler.spawn("Missile", transform.position + spawnOffset);
            missile.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            CameraController.needUpdate = true;
        }
	}
}
