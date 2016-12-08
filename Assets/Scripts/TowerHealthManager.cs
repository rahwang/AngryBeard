using UnityEngine;
using System.Collections;

public class TowerHealthManager : MonoBehaviour {
    int towerHealth = 100;
    GameManager gameManager;
    public GameObject Heart;
    Object[] hearts;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hearts = new GameObject[10];
        for (int i = 0; i < 10; i++) {
            hearts[i] = Instantiate(Heart, Vector3.zero, Quaternion.Euler(0, 36 * i, 0));
        }
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
            Destroy(hearts[towerHealth / 10]);
            GetComponent<AudioSource>().Play();
            if (towerHealth <= 0) {
                gameManager.playing = false;
            }
		}
	}
}
