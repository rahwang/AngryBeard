using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public GameObject enemy_prefab;
	public float spawn_distance = 10.0f;

	// Use this for initialization
	void Start () {
		Spawn (10, enemy_prefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn(int spawn_count, GameObject enemy_prefab) {
		for (int i = 0; i < spawn_count; ++i) {
			// Generate random direction
			Vector3 dir = (new Vector3 ((Random.value * 2) - 1, 0, (Random.value * 2) - 1)).normalized;
			Vector3 spawn_pos = dir * spawn_distance;
			Quaternion spawn_rot = Quaternion.LookRotation (-(spawn_pos));
			Instantiate (enemy_prefab, spawn_pos, spawn_rot);
		}
	}
}
