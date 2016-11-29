using UnityEngine;
using System.Collections;

public class TowerHealthManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.name.Contains ("Enemy")) {
			EventManager.TriggerEvent ("towerTakeDamage");
			Destroy (col.gameObject);
			Debug.Log ("Tower hit");
		}
	}
}
