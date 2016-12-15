using UnityEngine;
using System.Collections;

public class TowerHealthManager : MonoBehaviour {
    public int towerHealth = 100;
    GameManager gameManager;
    public GameObject Heart;
    Object[] hearts;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hearts = new GameObject[10];
        for (int i = 0; i < 10; i++) {
            hearts[i] = Instantiate(Heart, Vector3.up*0.07f, Quaternion.Euler(0, 36 * i, 0));
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
            for (int i = 0; i < 10; i++) {
                float offset = 0.18f - (towerHealth / 100.0f) * 0.36f;
                ((GameObject)hearts[i]).GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset));
            }
            GetComponent<AudioSource>().Play();
            if (towerHealth <= 0) {
                gameManager.LoseGame();
            }
		}
	}
}
