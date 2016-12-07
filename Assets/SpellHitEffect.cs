using UnityEngine;
using System.Collections;

public class SpellHitEffect : MonoBehaviour {
    public SpellManager.Element type;

	// Use this for initialization
	void Start () {
        Invoke("SelfDestruct", 2.0f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("Enemy")) {
            col.gameObject.GetComponent<EnemyScript>().effect = type;

            switch (type) {
                case SpellManager.Element.Fire:
                    col.gameObject.GetComponent<EnemyScript>().health -= 20;
                    break;
                case SpellManager.Element.Earth:
                    col.gameObject.GetComponent<EnemyScript>().health -= 10;
                    break;
                case SpellManager.Element.Lightning:
                    col.gameObject.GetComponent<EnemyScript>().health -= 20;
                    break;
                case SpellManager.Element.Frost:
                    col.gameObject.GetComponent<EnemyScript>().health -= 10;
                    break;
                default: break;
            }

            if (col.gameObject.GetComponent<EnemyScript>().health <= 0) {
                Destroy(col.gameObject);
            }
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
