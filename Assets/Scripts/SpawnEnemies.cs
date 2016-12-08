using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public GameObject enemy_prefab;
	public float spawn_distance;
    public float spawn_frequency;
    public int num_enemies;

    GameObject gameManager;
    int enemies_spawned;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        enemies_spawned = 0;
	}

    // Update is called once per frame
    float startTime = Time.time;
    void Update() {
        // Number of enemies on level
        num_enemies = gameManager.GetComponent<GameManager>().level + 19;

        // Spawn enemies if there are enemies left AND enough time has passed
        if (enemies_spawned < num_enemies && Time.time - startTime > spawn_frequency) {
                Spawn(enemy_prefab);
                startTime = Time.time;
                enemies_spawned++;
        }
    }

	void Spawn(GameObject enemy_prefab) {
		Vector3 dir = (new Vector3 ((Random.value * 2) - 1, 0, (Random.value * 2) - 1)).normalized;
		Vector3 spawn_pos = dir * spawn_distance + Vector3.up*0.8f;
		Quaternion spawn_rot = Quaternion.LookRotation (-(spawn_pos));
		Instantiate (enemy_prefab, spawn_pos, spawn_rot);
	}
}
