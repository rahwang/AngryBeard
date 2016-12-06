using UnityEngine;
using System.Collections;

public class SpellHitEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SelfDestruct", 2.0f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("Enemy"))
        {
            Destroy(col.gameObject);
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
