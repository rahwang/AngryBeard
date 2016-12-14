using UnityEngine;
using System.Collections;

public class SpellHitEffect : MonoBehaviour {
    public SpellManager.Element type;
    GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        Invoke("SelfDestruct", 2.0f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy")) {
            col.gameObject.GetComponent<EnemyScript>().effect = type;
            int lostHealth = 0;

            switch (type) {
                case SpellManager.Element.Fire:
                    lostHealth = 20;
                    break;
                case SpellManager.Element.Earth:
                    lostHealth = 10;
                    break;
                case SpellManager.Element.Lightning:
                    lostHealth = 20;
                    break;
                case SpellManager.Element.Frost:
                    lostHealth = 10;
                    break;
                default: break;
            }

            int numHearts = col.gameObject.GetComponent<EnemyScript>().health / 10;
            col.gameObject.GetComponent<EnemyScript>().health -= lostHealth;
            for (int i = 0; i < lostHealth / 10; i++) {
                if (numHearts <= 0) break;
                Destroy(col.gameObject.GetComponent<EnemyScript>().enemyHealth[numHearts - 1]);
                numHearts--;
            }

            if (col.gameObject.GetComponent<EnemyScript>().health <= 0) {
                Destroy(col.gameObject);
                gameManager.GetComponent<GameManager>().numEnemiesKilled++;
                Debug.Log(gameManager.GetComponent<GameManager>().numEnemiesKilled);
            }
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
