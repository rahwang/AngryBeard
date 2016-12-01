using UnityEngine;
using System.Collections;

public class TowerHealthManager : MonoBehaviour {
    int towerHealth = 100;
    GameManager gameManager;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.name.Contains ("Enemy")) {
			EventManager.TriggerEvent ("towerTakeDamage");
			Destroy (col.gameObject);
			Debug.Log ("Tower hit");
            towerHealth -= 10;
            if (towerHealth <= 0) {
                gameManager.playing = false;
            }
		}
	}
}
